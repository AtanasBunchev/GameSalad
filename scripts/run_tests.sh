#!/bin/sh

docker build -t dotnet-docker-image-test --progress=plain --no-cache --target build .
