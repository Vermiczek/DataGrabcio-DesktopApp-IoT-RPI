#!/usr/bin/php
<?php
	

$ledDisplay = array();
$ledDisplayDataFile = 'led_displayCs.json';
$n=0;
	

for($i=0;$i<8;$i++)
   for($j=0;$j<8;$j++){
       $ledDisplayPixel = array($j,$i,0,0,0);
       array_push($ledDisplay,$ledDisplayPixel);
    }
$ledDisplayJson=json_encode($ledDisplay, JSON_NUMERIC_CHECK);
$dataFile = fopen($ledDisplayDataFile, 'w+') or die("ERR1");
fwrite($dataFile, $ledDisplayJson);
fclose($dataFile);

echo "ACK1 ";
	shell_exec("sudo ./led_displayCs.py");
	echo "ACK2 ";

?>