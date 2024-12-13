# README

## Description

Ce projet illustre une application client-serveur permettant :

- **Backend (C#/.NET)**
    - Authentification via un endpoint `/api/v1/auth` avec `admin/admin`.
    - Obtention d’un token JWT valable 1h.
    - Consultation d’une liste de services via `/api/v1/services`.
    - Récupération des actions disponibles pour un service spécifique via `/api/v1/services/{service_name}`.
    - Exécution d’une action avec `/api/v1/services/{service_name}` (POST), qui lance un job asynchrone de 3min simulant l’exécution d’une action. Un `jobId` est retourné immédiatement.
    - Pendant les 3min, des logs sont générés. Ils sont accessibles via une WebSocket `/api/v1/ws?id={jobId}`.

- **Frontend (React / Node.js)**
    - Page de login.
    - Une fois connecté, un menu affiche la liste des services.
    - Au clic sur un service, on obtient et affiche les actions possibles.
    - Un clic sur une action lance le job associé côté backend et ouvre une WebSocket pour afficher en temps réel les logs produits.


## Prérequis

- Docker et docker-compose installés
- Port 8080 disponible pour le backend
- Port 3000 disponible pour le frontend

## Lancement du Projet

1. Cloner le dépôt :
   ```bash
   git clone https://github.com/QuentinROUVILLE/OlymppTestProject
   cd OlymppTestProject
   ```

2. Lancer `docker-compose` :
   ```bash
   docker-compose up --build
   ```

## Utilisation

1. Ouvrir le navigateur sur `http://localhost:3000`.
2. Se connecter avec `admin/admin`.

## Optimisations possibles

- Afin de faciliter l'installation et le lancement du projet, les secrets ont été écrit en dur dans le code et pourraient être référencés dans un fichier de configuration.
- Le front pourrait fournir plus de feedback en cas d'erreur.
- Mettre en place des tests.
