<?php
ini_set("display_errors", "1");
ini_set("display_startup_errors", "1");
error_reporting(E_ALL);
require_once "bootstrap.php";
require_once __DIR__ . "/vendor/autoload.php";

use App\Entity\WeatherStation;

// Standard-Zeitzone festlegen
date_default_timezone_set("Europe/Berlin");

$server   = "";
$port     = 8883;
$clientId = "db-client";

/**
 * Erstellt eine Instanz des MQTT-Clients
 */
$mqtt = new \PhpMqtt\Client\MqttClient($server, $port, $clientId);

$stationRepository = $entityManager->getRepository(WeatherStation::class);

/**
 * Callback-Funktion für eingehende MQTT-Nachrichten.
 *
 * Diese Funktion wird aufgerufen, wenn eine neue MQTT-Nachricht empfangen wird.
 * Die Sensorwerte aus der Nachricht werden in einem entsprechenden Objekt abgespeichert
 * und in der Datenbank abgelegt, solange die Station dort vorhanden ist.
 * 
 * @param string $topic Die Topic, an den die Nachricht gesendet wurde.
 */
$msgCallback = function(string $topic, string $msg) use ($entityManager, $stationRepository) {
    $station = $stationRepository->findOneBy(array("topic" => $topic));
    if ($station) {
        $type = $station->getType();
        echo $msg.PHP_EOL;
        switch($type) {
            case "Outdoor":
                $data = OutdoorData::fromJson($station, $msg);
                break;
            case "Indoor":
                $data = IndoorData::fromJson($station, $msg);
                break;
            default:
                error_log("Unknown station type ".$type, 0);
                return;
        }

        $entityManager->persist($data);
        try {
            $entityManager->flush();
        } catch(Exception $e) {
            echo $e->getMessage()."\n";
        }
    }
};

/**
 * Verbindungseinstellungen für den MQTT-Client
 */
$connectionSettings = (new \PhpMqtt\Client\ConnectionSettings)
    ->setUserName("")
    ->setPassword("")
    ->setUseTls(true)
    ->setTlsCertificateAuthorityFile("");

/**
 * Verbindet sich mit dem MQTT-Broker mit TLS-Konfiguration
 *
 * @param bool $cleanSession Gibt an, ob eine clean Session für die Verbindung gestartet werden soll.
 */
$mqtt->connect($connectionSettings, true);

$stations = array();
$stations = $stationRepository->findAll();

/**
 * Abonniert die Topics für jede Wetterstation
 *
 * @param string $topic Die zu abonnierende Topic.
 * @param callable $msgCallback Die Callback-Funktion, die aufgerufen wird, wenn eine Nachricht empfangen wird.
 * @param int $qos Die Quality of Service (QoS) Stufe für das Abonnement.
 */
foreach ($stations as $station) {
    $mqtt->subscribe($station->getTopic(), $msgCallback, 0);
}

$mqtt->loop(true);
$mqtt->disconnect();
