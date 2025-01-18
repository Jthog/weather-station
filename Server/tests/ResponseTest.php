<?php
use App\Http\Response;
use PHPUnit\Framework\TestCase;

final class ResponseTest extends TestCase
{
    public function testSendWithDefaultStatus()
    {
        $data = ["message" => "Testnachricht"];
        $response = new Response();
        
        $this->expectOutputString(json_encode($data));
        $response->send($data);
        
        $this->assertEquals(200, http_response_code());
        $this->assertEquals("Content-Type: application/json", xdebug_get_headers()[0]);
    }

    public function testSendWithCustomStatus()
    {
        $data = ["message" => "Testnachricht mit anderem Status"];
        $status = 201;
        $response = new Response();
        
        $this->expectOutputString(json_encode($data));
        $response->send($data, $status);

        $this->assertEquals($status, http_response_code());
        $this->assertEquals("Content-Type: application/json", xdebug_get_headers()[0]);
    }

    public function testErrorWithDefaultStatus()
    {
        $message = "Ein Fehler ist aufgetreten.";
        $response = new Response();
        $expectedData = ["error" => $message];
        
        $this->expectOutputString(json_encode($expectedData));
        $response->error($message);

        $this->assertEquals(500, http_response_code());
        $this->assertEquals("Content-Type: application/json", xdebug_get_headers()[0]);
    }

        public function testErrorWithCustomStatus()
    {
        $message = "Ein anderer Fehler ist aufgetreten.";
        $status = 400;
        $response = new Response();
        $expectedData = ["error" => $message];
        
        $this->expectOutputString(json_encode($expectedData));
        $response->error($message, $status);

        $this->assertEquals($status, http_response_code());
        $this->assertEquals("Content-Type: application/json", xdebug_get_headers()[0]);
    }
}
