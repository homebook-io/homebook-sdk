# homebook-sdk
Developer SDK and CLI (dotnet tool) for HomeBook. Provides tooling to bootstrap, and prepare a local HomeBook development environment.

## install

install via dotnet tool

```
dotnet tool install -g homebook-sdk
```

this will install the `hbd` command globally.

## check

### dependencies

```
hbd check --dependencies
```

## setup

### database

```
hbd setup --database --db-type [postgresql|mysql]
```
