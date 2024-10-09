# RLInformaticaAPI (incompleta y pausada temporalmente)

Este es un proyecto para una empresa que necesitaba una aplicación web local para gestionar sus reparaciones o armados de PCs, y que permita ver qué empleado realizó cada reparación. Además, debe contar con un dashboard que muestre los ingresos y la cantidad de reparaciones hechas en el mes corriente, tanto para el empleado como para el jefe.

A esta API le falta parte del backend y sus pruebas unitarias e integración, pero la mayoria de requisitos funcionales ya estan hechos y probados con postman. También falta el frontend que estaba desarrollando con Angular. Me está llevando más tiempo del esperado porque estoy aprendiendo Angular al mismo tiempo que implemento todo en esta app web. He pausado el proyecto, que quedó incompleto, porque la universidad me está demandando más tiempo, ya que quiero aprobar la última materia que me falta para recibirme, y le estoy dedicando todo mi tiempo a esa materia. Por el lado de la pyme, me dijeron que necesitan la aplicación antes de 2025 para, a partir de ese año, empezar a utilizarla, así que no habría ningún problema.

La subo a GitHub para tenerla guardada y para que la gente interesada la vea, aclarando que está incompleta.

Tecnologias usadas: Estoy utilizando .NET 8 con base de datos SQL Server, usando Entity Framework Core como ORM. La API cuenta con sistema de usuarios hecho con Identity, donde hay un usuario superior (Jefe/Admin) y otro con menos permisos (Empleado), ademas cuenta con Json Web Token (JWT) para validar a cada usuario. Tambien utiliza AutoMapper para el mapeo automatico de clases.
