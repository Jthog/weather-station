<?php
ini_set('display_errors', '1');
ini_set('display_startup_errors', '1');
error_reporting(E_ALL);
require_once 'bootstrap.php';

date_default_timezone_set("Europe/Berlin");

$stationRepository = $entityManager->getRepository('WeatherStation');
$stationOut = $stationRepository->findOneBy(array("id" => 1));
$stationIn = $stationRepository->findOneBy(array("id" => 2));

$rowCount = 100;
$timestamp = time();

for ($i = 0; $i < $rowCount; $i++) {
    // Outdoor
    $data = new OutdoorData(
        $stationOut,
        $timestamp - ($rowCount - $i - 1) * 120,
        18.0 + rand(0, 50) / 10,
        0.4 + rand(0, 20) / 100,
        900 + rand(0, 2000) / 10
    );
    $entityManager->persist($data);
    try {
        $entityManager->flush();
    } catch(Exception $e) {
        echo $e->getMessage()."\n";
        break;
    }
    
    // Indoor
    $data = new IndoorData(
        $stationIn,
        $timestamp - ($rowCount - $i - 1) * 120,
        -2 + rand(0, 150) / 10,
        0.1 + rand(0, 70) / 100,
        500 + rand(0, 400)
    );
    $entityManager->persist($data);
    try {
        $entityManager->flush();
    } catch(Exception $e) {
        echo $e->getMessage()."\n";
        break;
    }
}

echo $rowCount." rows inserted\n";
