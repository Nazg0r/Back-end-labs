CREATE TABLE IF NOT EXISTS categories (
    id SERIAL PRIMARY KEY,
    name VARCHAR(255) NOT NULL
);

CREATE TABLE IF NOT EXISTS currencies (
    id SERIAL PRIMARY KEY,
    name VARCHAR(255) NOT NULL
);

CREATE TABLE IF NOT EXISTS users (
    id SERIAL PRIMARY KEY,
    currency_id INT DEFAULT 1,
    hash_password BYTEA NOT NULL,
    salt BYTEA NOT NULL,
    name VARCHAR(255) NOT NULL,
    CONSTRAINT fk_currency FOREIGN KEY (currency_id) REFERENCES currencies(id) ON DELETE SET NULL
);

CREATE TABLE IF NOT EXISTS records (
    id SERIAL PRIMARY KEY, 
    user_id INT NOT NULL,
    category_id INT NOT NULL,
    currency_id INT DEFAULT 1,                    
    creation_date VARCHAR(255) NOT NULL,          
    expenses_sum INT NOT NULL,                    
    CONSTRAINT fk_user FOREIGN KEY (user_id) REFERENCES users(id) ON DELETE CASCADE,
    CONSTRAINT fk_category FOREIGN KEY (category_id) REFERENCES categories(id) ON DELETE CASCADE,
    CONSTRAINT fk_currency FOREIGN KEY (currency_id) REFERENCES currencies(id) ON DELETE SET NULL
);

INSERT INTO currencies (id, name)
VALUES
    (1, 'USD'),
    (2, 'EUR'),
    (3, 'UAH')
ON CONFLICT (id) DO NOTHING;
