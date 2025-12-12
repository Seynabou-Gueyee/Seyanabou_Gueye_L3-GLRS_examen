-- 1. NETTOYAGE (Pour éviter les erreurs de duplication)

DROP TABLE IF EXISTS ligne_commande CASCADE;
DROP TABLE IF EXISTS livraison CASCADE;
DROP TABLE IF EXISTS commande CASCADE;
DROP TABLE IF EXISTS produit CASCADE;
DROP TABLE IF EXISTS utilisateur CASCADE;

DROP TYPE IF EXISTS role_type;
DROP TYPE IF EXISTS etat_commande;
DROP TYPE IF EXISTS etat_livraison;
DROP TYPE IF EXISTS type_produit;



-- 2. CRÉATION DES 4 ENUMS
CREATE TYPE role_type AS ENUM ('CLIENT', 'GESTIONNAIRE', 'LIVREUR');
CREATE TYPE etat_commande AS ENUM ('EN_COURS', 'VALIDEE', 'EN_PREPARATION', 'PRETE', 'LIVREE', 'ANNULEE');
CREATE TYPE etat_livraison AS ENUM ('EN_ATTENTE', 'EN_COURS', 'TERMINEE', 'ANNULEE');
CREATE TYPE type_produit AS ENUM ('BURGER', 'MENU', 'BOISSON', 'FRITE');



-- 3. CRÉATION DES TABLES
-- Table UTILISATEUR
CREATE TABLE utilisateur (
    id SERIAL PRIMARY KEY,
    nom VARCHAR(100) NOT NULL,
    prenom VARCHAR(100) NOT NULL,
    telephone VARCHAR(20),
    email VARCHAR(100) UNIQUE NOT NULL,
    mot_de_passe VARCHAR(255) NOT NULL,
    role role_type NOT NULL
);

-- Table PRODUIT (Burgers, Menus, etc.)
CREATE TABLE produit (
    id SERIAL PRIMARY KEY,
    nom VARCHAR(100) NOT NULL,
    description TEXT,
    prix DECIMAL(10, 2) NOT NULL,
    image_url VARCHAR(255),
    type type_produit NOT NULL,
    est_archivé BOOLEAN DEFAULT FALSE -- Pour ne plus afficher un produit sans le supprimer
);

-- Table COMMANDE
CREATE TABLE commande (
    id SERIAL PRIMARY KEY,
    date_commande TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    montant_total DECIMAL(10, 2) NOT NULL,
    etat etat_commande DEFAULT 'EN_COURS',
    client_id INTEGER REFERENCES utilisateur(id) ON DELETE SET NULL -- Lien vers le client
);

-- Table LIGNE_COMMANDE (Détails : quel produit dans quelle commande)
CREATE TABLE ligne_commande (
    id SERIAL PRIMARY KEY,
    quantite INTEGER NOT NULL,
    prix_unitaire DECIMAL(10, 2) NOT NULL,
    commande_id INTEGER REFERENCES commande(id) ON DELETE CASCADE,
    produit_id INTEGER REFERENCES produit(id) ON DELETE SET NULL
);

-- Table LIVRAISON
CREATE TABLE livraison (
    id SERIAL PRIMARY KEY,
    date_livraison TIMESTAMP,
    etat etat_livraison DEFAULT 'EN_ATTENTE',
    commande_id INTEGER UNIQUE REFERENCES commande(id) ON DELETE CASCADE, -- 1 livraison = 1 commande
    livreur_id INTEGER REFERENCES utilisateur(id) ON DELETE SET NULL -- Lien vers le livreur
);


-- 4. JEU DE DONNÉES DE TEST (Utilisateurs & Produits)
INSERT INTO utilisateur (nom, prenom, telephone, email, mot_de_passe, role) VALUES 
('Gueye', 'Seynabou', '770000001', 'admin@exam-l3glrs.sn', 'VOTRE_MOT_DE_PASSE', 'GESTIONNAIRE'),
('Diop', 'Moussa', '770000002', 'livreur1@exam-l3glrs.sn', 'pass123', 'LIVREUR'),
('Sow', 'Fatou', '770000003', 'client1@exam-l3glrs.sn', 'pass123', 'CLIENT');

-- Insertion de quelques PRODUITS
INSERT INTO produit (nom, description, prix, type) VALUES 
('Brasil Burger', 'Double steak, fromage, sauce spéciale', 3500, 'BURGER'),
('Menu Complet', 'Burger + Frites + Boisson', 5000, 'MENU'),
('Coca Cola', '33cl', 1000, 'BOISSON');


-- 5. VÉRIFICATION
SELECT * FROM utilisateur;