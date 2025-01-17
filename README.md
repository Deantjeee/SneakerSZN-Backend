# SneakerSZN-Backend

## Before starting the application

Run this command in the main project folder "SneakerSZN-Backend/SneakerSZN"
```cmd
dotnet ef database update --project SneakerSZN_DAL   --startup-project SneakerSZN
```

docker run --net sneakerszn_network --name sneakerszn_mysql -e MYSQL_DATABASE=sneakerszn_db -e MYSQL_ROOT_PASSWORD=dbpassword -p 3390:3306 -d mysql 

docker rm sneakerszn_be
docker build -t sneakerszn_api .
docker run -d -p 8090:8080 --net=sneakerszn_network --name=sneakerszn_be sneakerszn_api

docker compose build
docker compose up -d
docker compose up -d --build
