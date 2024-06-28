# Use a imagem oficial do PostgreSQL como base
FROM postgres

# Defina a variável de ambiente para a senha do superusuário (altere conforme necessário)
ENV POSTGRES_PASSWORD=123456

# Crie um banco de dados e um usuário
ENV POSTGRES_DB=CriEducSchool
ENV POSTGRES_USER=crieduc

# Copie os scripts SQL para o diretório de inicialização do banco de dados
COPY init.sql /docker-entrypoint-initdb.d/
