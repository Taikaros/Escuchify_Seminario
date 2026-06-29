Feature: Motor de búsqueda de Escuchify
  Como usuario de la aplicación
  Quiero poder buscar canciones por su título
  Para encontrar rápidamente la música que deseo escuchar

Scenario: Búsqueda de una canción existente en el catálogo
  Given que la canción "Radio Ga Ga" está registrada en la base de datos
  When el usuario ingresa la palabra "Radio" en la barra de búsqueda
  And presiona el botón de buscar
  Then el sistema debe devolver al menos un resultado
  And el resultado debe incluir la canción "Radio Ga Ga"