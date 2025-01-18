<?php
use App\Http\Request;
use PHPUnit\Framework\TestCase;

final class RequestTest extends TestCase
{
    #[RunInSeparateProcess]
    #[PreserveGlobalState(false)]
    public function testConstructorGetMethod()
    {
        // Globale Server-Variablen für den Test setzen
        $_SERVER["REQUEST_METHOD"] = "GET";
        $_SERVER["REQUEST_URI"] = "/testpfad?param1=wert1&param2=wert2";
        $_GET = ["param1" => "wert1", "param2" => "wert2"];

        $request = new Request();

        $this->assertEquals("GET", $request->method);
        $this->assertEquals("/testpfad", $request->uri);
        $this->assertEquals(["param1" => "wert1", "param2" => "wert2"], $request->data);
    }

    #[RunInSeparateProcess]
    #[PreserveGlobalState(false)]
    public function testConstructorPostMethod()
    {
        $_SERVER["REQUEST_METHOD"] = "POST";
        $_SERVER["REQUEST_URI"] = "/andererpfad";

        $request = new Request();

        $this->assertEquals("POST", $request->method);
        $this->assertEquals("/andererpfad", $request->uri);
        $this->assertEquals(array(), $request->data);
    }

    #[RunInSeparateProcess]
    #[PreserveGlobalState(false)]
    public function testConstructorPutMethod()
    {
        $_SERVER["REQUEST_METHOD"] = "PUT";
        $_SERVER["REQUEST_URI"] = "/noch/ein/pfad";

        $request = new Request();

        $this->assertEquals("PUT", $request->method);
        $this->assertEquals("/noch/ein/pfad", $request->uri);
        $this->assertEquals(array(), $request->data);
    }

    #[RunInSeparateProcess]
    #[PreserveGlobalState(false)]
    public function testConstructorWithQueryStringOnly()
    {
        $_SERVER["REQUEST_METHOD"] = "GET";
        $_SERVER["REQUEST_URI"] = "?param1=wert1";
        $_GET = ["param1" => "wert1"];

        $request = new Request();

        $this->assertEquals("GET", $request->method);
        $this->assertEquals("", $request->uri); // Leerer Pfad
        $this->assertEquals(["param1" => "wert1"], $request->data);
    }

    #[RunInSeparateProcess]
    #[PreserveGlobalState(false)]
    public function testConstructorWithEmptyUri()
    {
        $_SERVER["REQUEST_METHOD"] = "GET";
        $_SERVER["REQUEST_URI"] = "";
        $_GET = [];

        $request = new Request();

        $this->assertEquals("GET", $request->method);
        $this->assertEquals("", $request->uri);
        $this->assertEquals([], $request->data);
    }
}