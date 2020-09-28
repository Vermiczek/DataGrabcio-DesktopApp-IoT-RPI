#!/usr/bin/php
<?php
	

$ledDisplay = array();
$ledDisplayDataFile = 'led_displayCs.json';
$n=0;
	
$x=$_GET[x];
$y=$_GET[y];
$r=$_GET[r];
$g=$_GET[g];
$b=$_GET[b];

$ledDisplayx = array($x+0,$y+0,$r+0,$g+0,$b+0);
$ledDisplay= array($ledDisplayx);
$ledDisplayJson=json_encode($ledDisplay, JSON_NUMERIC_CHECK );

$dataFile = fopen($ledDisplayDataFile, 'w+') or die("ERR1");
fwrite($dataFile, $ledDisplayJson);
fclose($dataFile);

echo "ACK1 ";
	shell_exec("sudo ./led_displayCs.py");
	echo "ACK2 ";

?>