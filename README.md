### Used For Running Postgres DB W/ Docker
```ps1
docker run --name local -e POSTGRES_PASSWORD=password -p 5432:5432 -d postgres
```