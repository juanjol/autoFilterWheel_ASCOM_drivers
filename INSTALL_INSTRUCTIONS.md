# Instrucciones de Instalación del Driver ASCOM

## ✅ Requisitos Previos

1. **ASCOM Platform 6.0 o superior** instalado
   - Descargar desde: https://ascom-standards.org/Downloads/Index.htm
   - Instalar la versión Developer (incluye Runtime)

2. **Visual Studio 2019 o superior** (o MSBuild)

3. **Permisos de Administrador** para registrar drivers COM

## 🚀 Instalación Automática (Recomendado)

1. **Compilar** (si es necesario):
   ```
   Abrir Visual Studio → Abrir MotoFilterWheel.sln → Build → Build Solution
   ```

2. **Ejecutar como Administrador**:
   ```
   Click derecho en "install_driver.bat" → "Ejecutar como Administrador"
   ```

3. **Verificar instalación**:
   - Abrir ASCOM Device Hub
   - Filter Wheel → Choose
   - Buscar: "ASCOM FilterWheel Driver for juanjolMotoFilterWheel"
   - Si aparece = ✅ Instalación exitosa

## 🔧 Instalación Manual

Si el script automático falla:

1. **Abrir PowerShell como Administrador**
2. **Navegar al directorio**:
   ```powershell
   cd "C:\Users\juanjo\source\repos\MotoFilterWheel\MotoFilterWheel\bin\Release"
   ```
3. **Registrar el driver**:
   ```powershell
   .\ASCOM.juanjolMotoFilterWheel.exe /regserver
   ```

## 🎯 Configuración en NINA

1. **Abrir NINA**
2. **Equipment → Filter Wheel**
3. **Choose Device**
4. **Buscar**: "ASCOM FilterWheel Driver for juanjolMotoFilterWheel" o "ESP32 Filter Wheel"
5. **Setup/Properties**:
   - Seleccionar el puerto COM del ESP32 (ej: COM3)
   - Habilitar trace si necesitas debugging
6. **Connect**

## ❌ Solución de Problemas

### El driver no aparece en NINA:

**Problema 1: Driver no registrado**
```powershell
# Como Administrador:
cd "ruta\al\proyecto\bin\Release"
.\ASCOM.juanjolMotoFilterWheel.exe /regserver
```

**Problema 2: ASCOM Platform no instalado**
- Reinstalar ASCOM Platform 6.0+
- Reiniciar el sistema

**Problema 3: ComVisible = false**
- ✅ Ya corregido en el código

**Problema 4: Archivo no encontrado**
- Verificar que el build fue exitoso
- Revisar la carpeta: `MotoFilterWheel\bin\Release\`
- Debe existir: `ASCOM.juanjolMotoFilterWheel.exe`

### Error "Access Denied":
- Ejecutar PowerShell/CMD como Administrador
- Verificar que no hay antivirus bloqueando

### Driver aparece pero no conecta:
1. **Verificar hardware**:
   - ESP32 conectado al puerto COM correcto
   - Firmware funcionando (responde a comandos seriales)

2. **Verificar configuración**:
   - Abrir Properties del driver en NINA
   - Seleccionar puerto COM correcto
   - El driver verificará que el device ID sea `ESP32FW-5POS-V1.0`

3. **Ver logs**:
   - Habilitar trace en Properties del driver
   - Los logs están en: `C:\Users\[usuario]\Documents\ASCOM\Logs\`

## 🗑️ Desinstalación

Para desinstalar el driver:

```
Click derecho en "uninstall_driver.bat" → "Ejecutar como Administrador"
```

O manualmente:
```powershell
# Como Administrador:
cd "ruta\al\proyecto\bin\Release"
.\ASCOM.juanjolMotoFilterWheel.exe /unregserver
```

## 📝 Notas Importantes

- **Siempre ejecutar como Administrador** para registrar/desregistrar drivers COM
- **El driver verifica el Device ID** del Arduino (`ESP32FW-5POS-V1.0`)
- **Reiniciar NINA** después de instalar el driver
- **Los logs detallados** están disponibles habilitando trace en Properties

## ✅ Verificación Final

Después de la instalación:

1. **ASCOM Device Hub**:
   - Filter Wheel → Choose → Debe aparecer el driver
   - Properties → Configurar COM port → Connect → Debe conectar sin errores

2. **NINA**:
   - Equipment → Filter Wheel → Choose Device → Debe aparecer el driver
   - Setup → COM port → Connect → Debe mostrar 5 posiciones de filtro

3. **Prueba funcional**:
   - Mover entre posiciones 1-5
   - Los nombres deben aparecer según configuración Arduino
   - Los movimientos deben ejecutarse correctamente