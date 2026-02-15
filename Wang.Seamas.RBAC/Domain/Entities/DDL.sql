--- postgresql

-- 用户表
CREATE TABLE users (
                       id SERIAL PRIMARY KEY,
                       username VARCHAR(100) NOT NULL UNIQUE,
                       nickname varchar(30),
                       email VARCHAR(255),
                       password_hash VARCHAR(255) NOT NULL,
                       is_enabled BOOLEAN NOT NULL DEFAULT TRUE
);

-- 角色表
CREATE TABLE roles (
                       id SERIAL PRIMARY KEY,
                       code VARCHAR(100) NOT NULL UNIQUE,
                       name VARCHAR(100) NOT NULL,
                       is_enabled BOOLEAN NOT NULL DEFAULT TRUE
);

-- 菜单表
CREATE TABLE menus (
                       id SERIAL PRIMARY KEY,
                       name VARCHAR(100) NOT NULL,
                       code VARCHAR(100),
                       path VARCHAR(255),
                       parent_id INTEGER REFERENCES menus(id) ON DELETE SET NULL,
                       sort_order INTEGER NOT NULL DEFAULT 0,
                       is_enabled BOOLEAN NOT NULL DEFAULT TRUE
);

-- API 接口表
CREATE TABLE api_endpoints (
                               id SERIAL PRIMARY KEY,
                               url VARCHAR(255) NOT NULL UNIQUE,
                               api_group varchar(32),
                               description TEXT,
                               is_enabled BOOLEAN NOT NULL DEFAULT TRUE
);

-- 用户-角色关联表
CREATE TABLE user_roles (
                            user_id INTEGER NOT NULL REFERENCES users(id) ON DELETE CASCADE,
                            role_id INTEGER NOT NULL REFERENCES roles(id) ON DELETE CASCADE,
                            PRIMARY KEY (user_id, role_id)
);

-- 角色-菜单权限表
CREATE TABLE role_menu_permissions (
                                       role_id INTEGER NOT NULL REFERENCES roles(id) ON DELETE CASCADE,
                                       menu_id INTEGER NOT NULL REFERENCES menus(id) ON DELETE CASCADE,
                                       PRIMARY KEY (role_id, menu_id)
);

-- 用户-菜单权限（特例）
CREATE TABLE user_menu_permissions (
                                       user_id INTEGER NOT NULL REFERENCES users(id) ON DELETE CASCADE,
                                       menu_id INTEGER NOT NULL REFERENCES menus(id) ON DELETE CASCADE,
                                       is_allowed BOOLEAN NOT NULL,
                                       PRIMARY KEY (user_id, menu_id)
);

-- 角色-API 权限表
CREATE TABLE role_api_permissions (
                                      role_id INTEGER NOT NULL REFERENCES roles(id) ON DELETE CASCADE,
                                      api_endpoint_id INTEGER NOT NULL REFERENCES api_endpoints(id) ON DELETE CASCADE,
                                      PRIMARY KEY (role_id, api_endpoint_id)
);

-- 用户-API 权限（特例）
CREATE TABLE user_api_permissions (
                                      user_id INTEGER NOT NULL REFERENCES users(id) ON DELETE CASCADE,
                                      api_endpoint_id INTEGER NOT NULL REFERENCES api_endpoints(id) ON DELETE CASCADE,
                                      is_allowed BOOLEAN NOT NULL,
                                      PRIMARY KEY (user_id, api_endpoint_id)
);

-- 索引（提升查询性能）
CREATE INDEX idx_users_username ON users(username);
CREATE INDEX idx_users_is_enabled ON users(is_enabled);
CREATE INDEX idx_roles_is_enabled ON roles(is_enabled);
CREATE INDEX idx_menus_is_enabled ON menus(is_enabled);
CREATE INDEX idx_api_endpoints_url ON api_endpoints(url);