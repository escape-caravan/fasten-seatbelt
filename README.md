# fasten-seatbelt

## On the Raspberry Pi

To download the latest version of the container, type:

`docker pull nikneem/raspi-escape-fastenseatbelt:latest`

To download a specific version of container, in the previous command, replace `latest` with the version number.

Type the following command to run the container in test mode:

`docker run --privileged -it --rm -p 8000:80 --name fastenseatbelt nikneem/raspi-escape-fastenseatbelt:latest`

The container will run, and calls can be made to http://{ip-address-of-pi}:8000/api/statusled. If you would like to run the container as a daemon and have is automatically started after the boot sequence, type the following command:

`docker run --privileged -it -d --restart unless-stopped -p 8000:80 --name fastenseatbelt nikneem/raspi-escape-fastenseatbelt:latest`
