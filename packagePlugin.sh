﻿#!/bin/bash
if test -d BepInEX/; then
  rm -rf BepInEX/
fi 

mkdir BepInEX
mkdir BepInEX/plugins
cp bin/Debug/netstandard2.1/StartingTeleporter.dll BepInEX/plugins/


if [ -f StartingTeleporters.zip ]; then
  rm -rf StartingTeleporters.zip
fi

zip -r StartingTeleporters.zip icon.png manifest.json README.md CHANGELOG.md BepInEX/

if test -d BepInEX/; then
  rm -rf BepInEX/
fi 