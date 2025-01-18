<?php
use App\Http\Request;
use App\Http\Response;
use App\Router\Router;
use PHPUnit\Framework\TestCase;

final class RouterTest extends TestCase
{
    #[RunInSeparateProcess]
    #[PreserveGlobalState(false)]
    public function testMissingRoute()
    {
        // Globale Server-Variablen für den Test setzen
        $_SERVER["REQUEST_METHOD"] = "GET";
        $_SERVER["REQUEST_URI"] = "/testpfad";
        $expectedData = ["error" => "Route not found"];

        $router = new Router();
        $request = new Request();
        $response = new Response();
        
        $this->expectOutputString(json_encode($expectedData));
        $router->handleRequest($request, $response);

        $this->assertEquals(404, http_response_code());
    }
    
    #[RunInSeparateProcess]
    #[PreserveGlobalState(false)]
    public function testGetRoute()
    {
        // Globale Server-Variablen für den Test setzen
        $_SERVER["REQUEST_METHOD"] = "GET";
        $_SERVER["REQUEST_URI"] = "/testpfad";
        $expectedData = array();
        $_GET = $expectedData;
        
        $testCallback = function(Request $request, Response $response) {
            $response->send($request->data);
        };

        $router = new Router();
        $request = new Request();
        $response = new Response();
        
        $router->addRoute("GET", "/testpfad", $testCallback);
        
        $this->expectOutputString(json_encode($expectedData));
        $router->handleRequest($request, $response);

        $this->assertEquals(200, http_response_code());
    }
    
    #[RunInSeparateProcess]
    #[PreserveGlobalState(false)]
    public function testGetRoutewithQueryString()
    {
        // Globale Server-Variablen für den Test setzen
        $_SERVER["REQUEST_METHOD"] = "GET";
        $_SERVER["REQUEST_URI"] = "/testpfad?message=Testnachricht";
        $expectedData = ["message" => "Testnachricht"];
        $_GET = $expectedData;
        
        $testCallback = function(Request $request, Response $response) {
            $response->send($request->data);
        };

        $router = new Router();
        $request = new Request();
        $response = new Response();
        
        $router->addRoute("GET", "/testpfad", $testCallback);
        
        $this->expectOutputString(json_encode($expectedData));
        $router->handleRequest($request, $response);

        $this->assertEquals(200, http_response_code());
    }
    
    #[RunInSeparateProcess]
    #[PreserveGlobalState(false)]
    public function testPostRoute()
    {
        // Globale Server-Variablen für den Test setzen
        $_SERVER["REQUEST_METHOD"] = "POST";
        $_SERVER["REQUEST_URI"] = "/testpfad";
        
        $testCallback = function(Request $request, Response $response) {
            $response->send($request->data);
        };

        $router = new Router();
        $request = new Request();
        $response = new Response();
        
        $router->addRoute("POST", "/testpfad", $testCallback);
        
        $this->expectOutputString("[]");
        $router->handleRequest($request, $response);

        $this->assertEquals(200, http_response_code());
    }
    
    #[RunInSeparateProcess]
    #[PreserveGlobalState(false)]
    public function testPutRoute()
    {
        // Globale Server-Variablen für den Test setzen
        $_SERVER["REQUEST_METHOD"] = "PUT";
        $_SERVER["REQUEST_URI"] = "/testpfad";
        
        $testCallback = function(Request $request, Response $response) {
            $response->send($request->data);
        };

        $router = new Router();
        $request = new Request();
        $response = new Response();
        
        $router->addRoute("PUT", "/testpfad", $testCallback);
        
        $this->expectOutputString("[]");
        $router->handleRequest($request, $response);

        $this->assertEquals(200, http_response_code());
    }

}