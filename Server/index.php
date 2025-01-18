<?php
// Dieses Skript stellt RESTful-Endpunkte für den Zugriff auf Wetterdaten der Wetterstationen bereit.

ini_set("display_errors", "1");
ini_set("display_startup_errors", "1");
error_reporting(E_ALL);
require_once "bootstrap.php";
require_once __DIR__ . "/vendor/autoload.php";

use App\Controller\SensorDataController;
use App\Http\Request;
use App\Http\Response;
use App\Router\Router;

// Setzt die Standardzeitzone
date_default_timezone_set("Europe/Berlin");

$request = new Request();
$response = new Response();
$router = new Router();
$SensorDataController = new SensorDataController($entityManager);

// Routen erstellen 
$router->addRoute("GET", "/outdoor/current", [$SensorDataController, "getOutCurrent"]);
$router->addRoute("GET", "/indoor/current", [$SensorDataController, "getInCurrent"]);

// Anfrage bearbeiten
$router->handleRequest($request, $response);
