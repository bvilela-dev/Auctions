# Microservices Auctions App

This is the repository for the Microservices 'Auctions' app.

You can run this app locally on your computer by following these instructions:

1. Using your terminal or command prompt clone the repo onto your machine in a user folder 

```
git clone https://github.com/bvilela-dev/Auctions.git
```
2. Change into the Auctions directory
```
cd git clone https://github.com/bvilela-dev/Auctions.git

```
3. Ensure you have Docker Desktop installed on your machine.  If not download and install from Docker and review their installation instructions for your Operating system [here](https://docs.docker.com/desktop/).
4. Build the services locally on your computer by running (NOTE: this may take several minutes to complete):
```
docker compose build
```
5. Once this completes you can use the following to run the services:
```
docker compose up -d
```
6. To see the app working you will need to provide it with an SSL certificate.   To do this please install 'mkcert' onto your computer which you can get from [here](https://github.com/FiloSottile/mkcert).  Once you have this you will need to install the local Certificate Authority by using:
```
mkcert -install
```
7. You will then need to create the certificate and key file on your computer to replace the certificates that I used.   You will need to change into the 'devcerts' directory and then run the following command:
```
cd devcerts
mkcert -key-file auctions.local.key -cert-file auctions.local.crt app.auctions.local api.auctions.local id.auctions.local
```
8.  You will also need to create an entry in your host file so you can reach the app by its domain name.   Please use this [guide](https://www.hostinger.com/tutorials/how-to-edit-hosts-file) if you do not know how to do this.  Create the following entry:
```
127.0.0.1 id.auctions.local app.auctions.local api.auctions.local
```
9. You should now be able to browse to the app on https://app.auctions.local
