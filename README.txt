Sistema de Clientes - Requisitos de Instalación

Antes de instalar el Sistema de Clientes, asegúrese de tener instalado:

1. .NET 6.0 Runtime
   - Descargue e instale desde: https://dotnet.microsoft.com/download/dotnet/6.0
   - Seleccione "Runtime" y descargue la versión para Windows x64
   - Ejecute el instalador y siga las instrucciones

2. Después de instalar el Runtime, puede instalar el Sistema de Clientes:
   - Ejecute el instalador SistemaClientesSetup.exe
   - Siga las instrucciones en pantalla
   - Se creará un acceso directo en el escritorio

Para cualquier problema durante la instalación, contacte al soporte técnico.

====================================================
INSTRUCCIONES PARA DESARROLLADORES
====================================================

Para trabajar en el desarrollo del proyecto, siga estos pasos:

1. Requisitos Previos:
   - Visual Studio 2022 (o versión compatible con .NET 6.0)
   - .NET 6.0 SDK (descargar desde: https://dotnet.microsoft.com/download/dotnet/6.0)
   - Git (descargar desde: https://git-scm.com/downloads)
   - Inno Setup (descargar desde: https://jrsoftware.org/isdl.php)

2. Clonar el Repositorio:
   - Abra una terminal (PowerShell o CMD)
   - Ejecute: git clone [URL_DEL_REPOSITORIO]
   - Navegue a la carpeta del proyecto: cd puntoDeVentaWindows

3. Configurar el Proyecto:
   - Abra la solución en Visual Studio
   - Restaure los paquetes NuGet (click derecho en la solución -> Restaurar paquetes NuGet)
   - Compile el proyecto (Build -> Build Solution)

4. Para Generar el Instalador:
   - Compile el proyecto en modo Release
   - Abra el archivo Setup.iss en Inno Setup
   - Compile el script (Build -> Compile)

Notas Importantes:
- Asegúrese de que la base de datos (clientes.db) esté en la ubicación correcta
- El instalador se generará en la carpeta Output/
- Para desarrollo, puede ejecutar el proyecto directamente desde Visual Studio

====================================================
INSTRUCCIONES ALTERNATIVAS (SIN VISUAL STUDIO)
====================================================

Si prefiere no usar Visual Studio, puede seguir estos pasos:

1. Requisitos Previos:
   - .NET 6.0 SDK (descargar desde: https://dotnet.microsoft.com/download/dotnet/6.0)
   - Git (descargar desde: https://git-scm.com/downloads)
   - Inno Setup (descargar desde: https://jrsoftware.org/isdl.php)
   - Un editor de código (recomendados):
     * Visual Studio Code (https://code.visualstudio.com/)
     * JetBrains Rider (https://www.jetbrains.com/rider/)
     * Notepad++ (https://notepad-plus-plus.org/)

2. Clonar y Configurar:
   - Abra una terminal (PowerShell o CMD)
   - Ejecute: git clone [URL_DEL_REPOSITORIO]
   - Navegue a la carpeta del proyecto: cd puntoDeVentaWindows
   - Restaure las dependencias: dotnet restore
   - Compile el proyecto: dotnet build

3. Ejecutar el Proyecto:
   - Para desarrollo: dotnet run
   - Para compilar en modo Release: dotnet publish -c Release

4. Para Generar el Instalador:
   - Asegúrese de que la compilación Release esté completa
   - Abra el archivo Setup.iss en Inno Setup
   - Compile el script (Build -> Compile)

Notas Adicionales:
- Si usa VS Code, instale la extensión "C#" para mejor soporte
- Los comandos dotnet se ejecutan desde la terminal en la carpeta del proyecto
- Para depurar, puede usar el depurador integrado de VS Code o JetBrains Rider

====================================================
CONFIGURACIÓN INICIAL DEL PROYECTO
====================================================

Después de clonar el proyecto, es importante realizar la siguiente configuración:

1. Base de Datos:
   - La base de datos clientes.db debe estar en la carpeta raíz del proyecto
   - Si la base de datos no existe, se creará automáticamente al ejecutar el programa
   - Asegúrese de que la aplicación tenga permisos de escritura en la carpeta

2. Configuración del Proyecto:
   - Verifique que el archivo PuntoDeVenta.csproj tenga todas las dependencias necesarias
   - Si hay errores de compilación, ejecute 'dotnet restore' para actualizar los paquetes
   - Para desarrollo, use el perfil de depuración (Debug)

3. Estructura de Carpetas:
   - bin/: Contiene los archivos compilados (no modificar manualmente)
   - obj/: Archivos temporales de compilación (no modificar)
   - Output/: Carpeta donde se genera el instalador

4. Solución de Problemas Comunes:
   - Si hay errores de compilación, verifique que .NET 6.0 SDK esté instalado correctamente
   - Si la base de datos no se crea, verifique los permisos de la carpeta
   - Si el instalador no se genera, asegúrese de que Inno Setup esté instalado correctamente

5. Desarrollo:
   - Use el modo Debug para desarrollo y pruebas
   - Use el modo Release para generar el instalador final
   - Mantenga una copia de seguridad de la base de datos antes de hacer cambios importantes 