<?php
namespace App\Entity;
use Doctrine\ORM\Mapping as ORM;

/**
 * @brief Wetterdaten für Innenräume.
 *
 * Diese Klasse erweitert die Basisklasse WeatherData und enthält zusätzlich
 * eine Messung des CO2-Gehalts der Luft.
 *
 * @extends WeatherData
 */
#[ORM\Entity]
#[ORM\Table(name: "indoor_data")]
class IndoorData extends WeatherData {
    /**
     * CO2-Gehalt in ppm.
     */
    #[ORM\Column(type: "float")]
    private float $co2;
    
    /**
     * Konstruktor für IndoorData.
     *
     * @param WeatherStation $station Die Wetterstation, zu der die Daten gehören.
     * @param int $timestamp Der Unix-Zeitstempel der Messung.
     * @param float $temperature Die gemessene Temperatur.
     * @param float $humidity Die gemessene Luftfeuchtigkeit.
     * @param float $co2 Der gemessene CO2-Gehalt.
     */
    public function __construct(
        WeatherStation $station, int $timestamp, float $temperature, float $humidity, float $co2
    ) {
        $this->station = $station;
        $time = new DateTime();
        $time->setTimestamp($timestamp);
        $this->timestamp = $time;
        $this->temperature = $temperature;
        $this->humidity = $humidity;
        $this->co2 = $co2;
    }
    
    /**
     * Erzeugt eine Instanz von IndoorData aus einem JSON-String.
     *
     * @param WeatherStation $station Die Wetterstation, zu der die Daten gehören.
     * @param string $json Der JSON-String.
     * @return ?IndoorData Eine Instanz von IndoorData oder null im Fehlerfall.
     */
    public static function fromJson(WeatherStation $station, string $json): ?self {
        $instance = null;
        if ($values = (array)json_decode($json)) {
            $instance = new self(
                $station,
                $values["timestamp"],
                $values["temperature"],
                $values["humidity"], 
                $values["co2"]
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
            "co2" => $this->co2,
        );
    }
}