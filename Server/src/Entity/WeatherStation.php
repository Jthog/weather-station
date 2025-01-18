<?php
namespace App\Entity;
use Doctrine\ORM\Mapping as ORM;

/**
 * @brief Beschreibt die Art der Wetterstation nach Sensorzusammensetzung.
 * 
 * Dieses Enum definiert die möglichen Typen von Wetterstationen (Indoor/Outdoor).
 * 
 */
enum StationType: string {
    /**
     * Aufzählungswert für eine Indoor-Station.
     */
    case Indoor = "Indoor";
    
    /**
     * Aufzählungswert für eine Außenstation.
     */
    case Outdoor = "Outdoor";
}

/**
 * @brief Klasse für die Wetterstation.
 * 
 * Diese Klasse repräsentiert eine Wetterstation mit ihren Eigenschaften wie Name, MQTT-Topic und Typ.
 */
#[ORM\Entity]
#[ORM\Table(name: "weather_station")]
class WeatherStation {
    /**
     * ID der Wetterstation.
     */
    #[ORM\Id, ORM\GeneratedValue, ORM\Column(type: "integer")]
    private int $id;
    
    /**
     * Name der Wetterstation (muss eindeutig sein).
     */
    #[ORM\Column(type: "string", unique: true)]
    private string $name;
    
    /**
     * MQTT-Topic der Wetterstation (muss eindeutig sein).
     */
    #[ORM\Column(type: "string", unique: true)]
    private string $topic;
    
    /**
     * Typ der Wetterstation (Indoor oder Outdoor).
     */
    #[ORM\Column(type: "string", enumType: StationType::class)]
    private $type;
    
    /**
     * Setzt den Namen der Wetterstation.
     *
     * @param string $stationName Der neue Name der Wetterstation.
     */
    public function setName(string $stationName): void {
        $this->name = $stationName;
    }
    
    /**
     * Setzt die MQTT-Topic der Wetterstation.
     *
     * @param string $topicName Das neue MQTT-Topic der Wetterstation.
     */
    public function setTopic(string $topicName): void {
        $this->topic = $topicName;
    }
    
     /**
     * Setzt den Typ der Wetterstation.
     *
     * @param string $type Der Typ der Wetterstation ("Indoor" oder "Outdoor").
     */
    public function setType(string $type): void {
        $this->type = StationType::tryFrom($type);
    }
    
    /**
     * Gibt die MQTT-Topic der Wetterstation zurück.
     *
     * @return string Die MQTT-Topic der Wetterstation.
     */
    public function getTopic(): string {
        return $this->topic;
    }
    
     /**
     * Gibt den Typ der Wetterstation zurück.
     *
     * @return string Der Typ der Wetterstation ("Indoor" oder "Outdoor").
     */
    public function getType(): string {
        return $this->type->value;
    }
    
    /**
     * Gibt die ID der Wetterstation zurück.
     *
     * @param string $stationName Der neue Name der Wetterstation.
     */
    public function getId(): int {
        return $this->id;
    }
}