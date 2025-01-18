<?php
namespace App\Http;
/**
 * @brief Klasse für eine HTTP-Anfrage.
 * 
 * Diese Klasse enthält Details zu einer Anfrage wie Methode, URI und
 * mitgesendete Daten und kann diese ausgeben.
 */
class Request {
    /**
     * Methode der HTTP-Anfrage.
     */
    public ?string $method;
    
    /**
     * URI der HTTP-Anfrage.
     */
    public ?string $uri;
    
    /**
     * Daten, die mit der HTTP-Anfrage übergeben wurden.
     */
    public array $data;

    /**
     * Konstruktor für eine Request-Klasse.
     */
    public function __construct() {
        $this->method = $_SERVER["REQUEST_METHOD"];
        $this->uri = parse_url($_SERVER["REQUEST_URI"], PHP_URL_PATH);
        $this->data = $this->parseData();
    }

    /**
     * Extrahiert die Daten aus der Anfrage.
     * 
     * @return array Die in der Anfrage enthaltenen Daten.
     */
    private function parseData(): array {
        // Switch-Statement ermöglicht einfache Erweiterung
        switch ($this->method) {
            case "GET":
                return $_GET;
            default:
                return array();
        }
    }
}