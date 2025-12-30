# Utilisation de PHP 8.4 avec Apache
FROM php:8.4-apache

# Installation des extensions nécessaires
RUN apt-get update && apt-get install -y \
    libicu-dev \
    libpq-dev \
    git \
    unzip \
    && docker-php-ext-install intl opcache pdo pdo_mysql pdo_pgsql

# Activation du module de réécriture d'URL d'Apache
RUN a2enmod rewrite

# Installation de Composer
COPY --from=composer:latest /usr/bin/composer /usr/bin/composer

WORKDIR /var/www/html
COPY . .

# Configuration Apache pour pointer vers le dossier /public de Symfony
ENV APACHE_DOCUMENT_ROOT /var/www/html/public
RUN sed -ri -e 's!/var/www/html!${APACHE_DOCUMENT_ROOT}!g' /etc/apache2/sites-available/*.conf
RUN sed -ri -e 's!/var/www/html!${APACHE_DOCUMENT_ROOT}!g' /etc/apache2/apache2.conf /etc/apache2/conf-available/*.conf

# Installation des dépendances (RETRAIT de --no-scripts pour activer les routes)
RUN composer install --no-dev --optimize-autoloader --ignore-platform-reqs

# Création des dossiers et gestion des permissions pour Symfony
RUN mkdir -p var/cache var/log public/uploads && \
    chown -R www-data:www-data var/ public/uploads && \
    chmod -R 775 var/ public/uploads

EXPOSE 80