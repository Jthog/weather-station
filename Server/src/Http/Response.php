<?php
namespace App\Http;
/**
 * @brief Klasse für eine Antwort.
 * 
 * Diese Klasse enthält Methoden zur Rückgabe einer Antwort.
 */
class Response {
    /**
     * Sendet eine Antwort an den Anfragensteller zurück.
     *
     * @param string|array $data Inhalt der Antwort.
     * @param int $status Der Statuscode, der mit der Antwort zurückgegeben wird.
     */
    public function send(array $data, int $status = 200): void {
        http_response_code($status);
        header("Content-Type: application/json");
        echo json_encode($data);
        return;
    }

    /**
     * Sendet eine Fehlermeldung an den Anfragensteller zurück.
     *
     * @param string $message Error-Nachricht.
     * @param int $status Error-Statuscode, der mit der Antwort zurückgegeben wird.
     */
    public function error(string $message, int $status = 500): void {
        $this->send(["error" => $message], $status);
        return;
    }
}