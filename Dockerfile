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

# On copie d'abord les fichiers
COPY . .

# Configuration Apache pour pointer vers le dossier /public
ENV APACHE_DOCUMENT_ROOT /var/www/html/public
RUN sed -ri -e 's!/var/www/html!${APACHE_DOCUMENT_ROOT}!g' /etc/apache2/sites-available/*.conf
RUN sed -ri -e 's!/var/www/html!${APACHE_DOCUMENT_ROOT}!g' /etc/apache2/apache2.conf /etc/apache2/conf-available/*.conf

# CRUCIAL : On prépare les dossiers AVANT le composer install
RUN mkdir -p var/cache var/log public/uploads && \
    chown -R www-data:www-data /var/www/html && \
    chmod -R 775 var/ public/uploads

# Installation des dépendances (SANS --no-scripts pour que Symfony s'auto-configure)
RUN composer install --no-dev --optimize-autoloader --ignore-platform-reqs

EXPOSE 80