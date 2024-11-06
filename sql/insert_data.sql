USE [BlogDb]

INSERT INTO [BlogDb].[dbo].[Categories] ([Name], [Description])
VALUES 
('Action', 'Films with intense physical activity and stunts.'),
('Comedy', 'Films designed to make audiences laugh.'),
('Drama', 'Films with a focus on realistic storytelling and emotional themes.'),
('Horror', 'Films intended to scare or thrill audiences.'),
('Sci-Fi', 'Films based on futuristic science and technology.'),
('Romance', 'Films focused on love and romantic relationships.'),
('Documentary', 'Non-fiction films documenting real events or people.'),
('Thriller', 'Films with suspenseful, plot-twisting storylines.'),
('Animation', 'Films created using animated art and visual effects.'),
('Fantasy', 'Films set in fictional worlds with magical elements.');


INSERT INTO [BlogDb].[dbo].[Hashtags] ([Name])
VALUES 
('#Action'),
('#Comedy'),
('#Drama'),
('#Horror'),
('#SciFi'),
('#Romance'),
('#Documentary'),
('#Thriller'),
('#Animation'),
('#Fantasy'),
('#Adventure'),
('#Biography'),
('#Crime'),
('#Mystery'),
('#War'),
('#Western'),
('#History'),
('#Musical'),
('#Family'),
('#Sport');
