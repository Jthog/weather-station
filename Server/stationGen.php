<?php
ini_set('display_errors', '1');
ini_set('display_startup_errors', '1');
error_reporting(E_ALL);
require_once 'bootstrap.php';

// Erstellt initiale Wetterstationen

$outdoorStation = new WeatherStation();
$outdoorStation->setName("outdoor_area");
$outdoorStation->setTopic("outdoor_station/data");
$outdoorStation->setType("Outdoor");
$entityManager->persist($outdoorStation);
$entityManager->flush();

$mainEntranceStation = new WeatherStation();
$mainEntranceStation->setName("main_entrance");
$mainEntranceStation->setTopic("indoor_station/data");
$mainEntranceStation->setType("Indoor");
$entityManager->persist($mainEntranceStation);
$entityManager->flush();