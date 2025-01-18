<?php
namespace App\Entity;
use Doctrine\ORM\Mapping as ORM;

/**
 * @brief Wetterdaten für Wetterstationen im Außeneinsatz.
 * 
 * Diese Klasse erweitert die Basisklasse WeatherData und enthält zusätzlich
 * eine Messung des Luftdrucks.
 * 
 * @extends WeatherData
 */
#[ORM\Entity]
#[ORM\Table(name: "outdoor_data")]
class OutdoorData extends WeatherData {
    /**
     * Luftdruckmessung in hPa.
     */
    #[ORM\Column(type: "float")]
    private float $pressure;
    
    /**
     * Konstruktor für OutdoorData.
     *
     * @param WeatherStation $station Die Wetterstation, zu der die Daten gehören.
     * @param int $timestamp Der Unix-Zeitstempel der Messung.
     * @param float $temperature Die gemessene Temperatur.
     * @param float $humidity Die gemessene Luftfeuchtigkeit.
     * @param float $pressure Der gemessene Luftdruck.
     */
    public function __construct(
        WeatherStation $station, int $timestamp, float $temperature, float $humidity, float $pressure
    ) {
        $this->station = $station;
        $time = new DateTime();
        $time->setTimestamp($timestamp);
        $this->timestamp = $time;
        $this->temperature = $temperature;
        $this->humidity = $humidity;
        $this->pressure = $pressure;
    }
    
    /**
     * Erzeugt eine Instanz von OutdoorData aus einem JSON-String.
     *
     * @param WeatherStation $station Die Wetterstation, zu der die Daten gehören.
     * @param string $json Der JSON-String.
     * @return ?OutdoorData Eine Instanz von OutdoorData oder null im Fehlerfall.
     */
    public static function fromJson(WeatherStation $station, string $json): ?self {
        $instance = null;
        if ($values = (array)json_decode($json)) {
            $instance = new self(
                $station,
                $values["timestamp"],
                $values["temperature"],
                $values["humidity"], 
                $values["pressure"]
            );
        }
        return $instance;
    }
    
    /**
     * Gibt die Sensordaten als assoziatives Array zurück.
     *
     * @return array Ein assoziatives Array mit den Sensordaten.
     */
    public function getSensorData(): array {
        return array(
            "timestamp" => $this->timestamp,
            "temperature" => $this->temperature,
            "humidity" => $this->humidity,
            "pressure" => $this->pressure,
        );
    }
}