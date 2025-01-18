<?php
namespace App\Entity;
use Doctrine\ORM\Mapping as ORM;

/**
 * @brief Basisklasse für Sensordaten einer Wetterstation.
 * 
 * Diese abstrakte Klasse definiert die gemeinsamen Eigenschaften und Methoden
 * für verschiedene Arten von Wetterdaten, die von einer Wetterstation erfasst werden können.
 */
#[ORM\MappedSuperclass]
abstract class WeatherData {
    /**
     * ID des Datensatzes.
     */
    #[ORM\Id, ORM\GeneratedValue, ORM\Column(type: "integer")]
    protected int $id;
    
    /**
     * Die ID der Wetterstation, zu der die Daten gehören.
     */
    #[ORM\ManyToOne(targetEntity: "WeatherStation")]
    #[ORM\JoinColumn(name: "station_id", referencedColumnName: "id")]
    protected WeatherStation $station;
    
    /**
     * Zeitstempel der Datenerfassung.
     */
    #[ORM\Column(type: "datetime")]
    protected DateTime $timestamp;
    
    /**
     * Gemessene Umgebungstemperatur in °C.
     */
    #[ORM\Column(type: "float")]
    protected float $temperature;
    
    /**
     * Gemessene Luftfeuchtigkeit in %.
     */
    #[ORM\Column(type: "float")]
    protected float $humidity;
    
    /**
     * Erzeugt ein Objekt aus einem JSON-String.
     * 
     * Diese Methode muss von der Unterklasse implementiert werden, um
     * die spezifische JSON-Struktur zu verarbeiten.
     *
     * @param WeatherStation $station Die Wetterstation, zu der die Daten gehören.
     * @param string $json Der JSON-String.
     * @return ?WeatherData Eine Instanz der abgeleiteten Klasse oder null im Fehlerfall.
     */
    abstract public static function fromJson(WeatherStation $station, string $json): ?self;
    
    /**
     * Gibt die Sensordaten zurück.
     * 
     * Diese Methode muss von den Unterklassen implementiert werden.
     *
     * @return array Liste mit Sensordaten.
     */
    abstract public function getSensorData(): array;
}