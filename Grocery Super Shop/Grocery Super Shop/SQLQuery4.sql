-- Create Reviews table if it doesn't exist yet
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Reviews' and xtype='U')
CREATE TABLE Reviews (
    ReviewID INT IDENTITY(1,1) PRIMARY KEY,
    ShopName NVARCHAR(100) NOT NULL,
    ReviewText NVARCHAR(MAX) NOT NULL
);

-- Insert 10 sample reviews
INSERT INTO Reviews (ShopName, ReviewText) VALUES
('Green Valley Grocery', 'The organic vegetables and fresh fruits at Green Valley are always top-notch and reasonably priced.'),
('Green Valley Grocery', 'Great neighborhood market, but the checkout queue can get a bit long during evening hours.'),
('Daily Fresh SuperShop', 'Found all my weekly grocery essentials in one go. The imported snacks section is amazing!'),
('Daily Fresh SuperShop', 'Very clean aisles and polite staff. Always a pleasant shopping experience here.'),
('City Supermart', 'The fresh meat and poultry counter is exceptionally clean. Best quality in town.'),
('City Supermart', 'Prices on household cleaning supplies are very competitive compared to other local stores.'),
('Mega Mart Express', 'Open late hours which is a lifesaver! Got what I needed right before midnight.'),
('Mega Mart Express', 'The dairy products are always well-stocked and fresh. Highly recommended.'),
('Corner Bazaar', 'A cozy little grocery shop with friendly owners. They always have fresh bread every morning.'),
('Corner Bazaar', 'Good variety of spices and local condiments. Very convenient location.');