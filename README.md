# fasten-seatbelt

## Install Docker
To run this image on your Raspberry Pi (arm 32) you need to have docker installed. To install docker:

> **Update & Upgrade**
> This step is optional, yet advised `sudo apt-get update && sudo apt-get upgrade`

`curl -sSL https://get.docker.com | sh`

## On the Raspberry Pi

To download the latest version of the container, type:

`sudo docker pull nikneem/raspi-escape-fastenseatbelt:latest`

To download a specific version of container, in the previous command, replace `latest` with the version number.

Type the following command to run the container in test mode:

`sudo docker run --privileged -it --rm -p 8000:80 --name fastenseatbelt nikneem/raspi-escape-fastenseatbelt:latest`

The container will run, and calls can be made to http://{ip-address-of-pi}:8000/api/statusled. If you would like to run the container as a daemon and have is automatically started after the boot sequence, type the following command:

`sudo docker run --privileged -it -d --restart unless-stopped -p 8000:80 --name fastenseatbelt nikneem/raspi-escape-fastenseatbelt:latest`
