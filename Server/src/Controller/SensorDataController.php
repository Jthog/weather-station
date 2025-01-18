<?php
namespace App\Controller;

use App\Http\Request;
use App\Http\Response;

use App\Entity\WeatherStation;
use App\Entity\IndoorData;
use App\Entity\OutdoorData;

use Doctrine\ORM\NoResultException;

/**
 * @brief Controller für Sensordaten.
 * 
 * Bietet Zugriff auf Sensordaten aus der Datenbank.
 */
class SensorDataController {
    /**
     * Letzte akzeptabler Zeitstempel für einen aktuellen Datensatz.
     */
    private string $recencyTresh;
    
    /**
     * Objekt zum Erstellen von Datenbankabfragen.
     */
    private $stationRepository;
    
    /**
     * Sammlung der erstellten Stationsobjekte.
     */
    private $queryBuilder;

    /**
     * Konstruktor für den SensorDataController.
     * 
     * @param EntityManager $entityManager
     */
    public function __construct($entityManager) {
        $this->recencyTresh = date("Y-m-d H:i:s", strtotime("30min ago"));
        $this->stationRepository = $entityManager->getRepository(WeatherStation::class);
        $this->queryBuilder = $entityManager->createQueryBuilder(); // EntityManager speichern
    }

    /**
     * Ermittelt die Stations-ID.
     * 
     * @param string $station Name der Station.
     * @return ?int ID der Station.
     */
    private function findStation(string $station): ?int {
        $name = $this->stationRepository->findOneBy(array("name" => $station));
        if ($name) {
            return $name->getId();
        }
        return null;
    }

    /**
     * Gibt den aktuellen Sensordatensatz einer Station aus.
     * 
     * @param int $station Stations-ID.
     * @param string $entityClass Klassenname für die angeforderten Sensordaten.
     * @param array $selectFields Abzufragende Felder eines Datensatzes.
     * @return ?array Assoziatives Array mit den Daten.
     */
    private function getCurrentData(int $station, string $entityClass, array $selectFields): ?array {
        $qb = $this->queryBuilder;
        $qb->select($selectFields)
           ->from($entityClass, "d")
           ->where("d.station = :station")
           ->andWhere("d.timestamp > :timestamp")
           ->orderBy("d.timestamp", "DESC")
           ->setMaxResults(1)
           ->setParameter("station", $station)
           ->setParameter("timestamp", $this->recencyTresh);

        try {
            return $qb->getQuery()->getSingleResult();
        } catch (NoResultException $e) {
            return null;
        }
    }

    /**
     * Gibt den aktuellen Sensordatensatz einer Station aus.
     * 
     * @param Request $request Anfrage-Objekt.
     * @param Response $response Antwort-Objekt.
     * @param string $entityClass Klassenname für die angeforderten Sensordaten.
     * @param array $selectFields Abzufragende Felder eines Datensatzes.
     */
    private function handleStationRequest(Request $request, Response $response, string $entityClass, array $selectFields): void {
        if (!isset($request->data["name"])) {
            $response->error("Invalid parameter", 404);
            return;
        }

        $station = $this->findStation($request->data["name"]);

        if ($station == null) {
            $response->error("Station not found", 404);
            return;
        }

        $data = $this->getCurrentData($station, $entityClass, $selectFields);

        if ($data === null) {
            $response->send([], 204);
        } else {
            $response->send($data);
        }
    }

     /**
     * Ruft den aktuellen Sensordatensatz einer Außenstation ab.
     * 
     * @param Request $request Anfrage-Objekt.
     * @param Response $response Antwort-Objekt.
     */
    public function getOutCurrent(Request $request, Response $response): void {
        $this->handleStationRequest(
            $request,
            $response,
            OutdoorData::class,
            ["d.timestamp", "d.temperature", "d.humidity", "d.pressure"]
        );
    }

    /**
     * Ruft den aktuellen Sensordatensatz einer Außenstation ab.
     * 
     * @param Request $request Anfrage-Objekt.
     * @param Response $response Antwort-Objekt.
     */
    public function getInCurrent(Request $request, Response $response): void {
        $this->handleStationRequest(
            $request,
            $response,
            IndoorData::class,
            ["d.timestamp", "d.temperature", "d.humidity", "d.co2"]
        );
    }
}