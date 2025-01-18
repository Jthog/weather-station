<?php
namespace App\Router;
use App\Http\Request;
use App\Http\Response;
/**
 * @brief Klasse zur Weiterleitung von HTTP-Anfragen.
 * 
 * Beinhaltet gesetzte Routen und arbeitet Anfragen ab.
 */
class Router {
    /**
     * Mögliche Routen für Anfragen.
     */
    private array $routes = [];

    /**
     * Sendet eine Antwort an den Anfragensteller zurück.
     *
     * @param string $method Methode der Anfrage.
     * @param string $uri URI der Anfrage.
     * @param $callback Funktion, die beim Aufruf ausgeführt wird.
     */
    public function addRoute(string $method, string $uri, $callback): void {
        $this->routes[$method][$uri] = $callback;
    }

    /**
     * Bearbeitet eine Anfrage.
     *
     * @param Request $request Anfrage-Objekt.
     * @param Response $response Antwort-Objekt.
     */
    public function handleRequest(Request $request, Response $response): void {
        if (isset($this->routes[$request->method][$request->uri])) {
            $callback = $this->routes[$request->method][$request->uri];
            call_user_func($callback, $request, $response);
        } else {
            //echo var_dump($request);
            $response->error("Route not found", 404);
        }
    }
}