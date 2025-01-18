<?php
// Erzeugt eine aktuellen Datensatz zu Testzwecken
date_default_timezone_set("Europe/Berlin");
$pdo = new PDO("sqlite:db.sqlite");
$now = date("Y-m-d H:i:s");
echo $now.PHP_EOL;

$statement = $pdo->prepare("INSERT INTO indoor_data (id, timestamp, temperature, humidity, co2, station_id)
    SELECT NULL AS id, ? AS timestamp, temperature, humidity, co2, station_id
    FROM indoor_data
    WHERE timestamp
        BETWEEN '2025-01-11 19:30:00' AND '2025-01-11 20:00:00'
    LIMIT 1");
$statement->execute(array($now));

$statement = $pdo->prepare("INSERT INTO outdoor_data (id, timestamp, temperature, humidity, pressure, station_id)
    SELECT NULL AS id, ? AS timestamp, temperature, humidity, pressure, station_id
    FROM outdoor_data
    WHERE timestamp
        BETWEEN '2025-01-11 18:30:00' AND '2025-01-11 19:00:00'
    LIMIT 1");
$statement->execute(array($now));