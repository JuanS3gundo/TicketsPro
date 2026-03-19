# 🌍 Sistema de Traducción - TicketPro ITSM Lite

Este directorio contiene los archivos de idioma para el sistema multiidioma de TicketPro.

## 📁 Estructura

```
I18n/
├── es-ES.lang          # Español (idioma por defecto)
├── en-US.lang          # Inglés
└── README.md           # Este archivo
```

## 🔧 Formato de Archivos .lang

Los archivos usan un formato simple `CLAVE=Valor`:

```
# Comentarios comienzan con #
Error=Error
Exito=Éxito
Advertencia=Advertencia
```

### Reglas:
- Una traducción por línea
- Formato: `CLAVE=Traducción`
- Las claves son **case-sensitive**
- Líneas que comienzan con `#` son comentarios
- Las claves NO deben contener espacios (usar `_` en su lugar)

## 🚀 Uso en el Código

### Traducir texto en formularios:

```csharp
// Método 1: Traducción directa
this.Text = LanguageBLL.Translate("Titulo_Del_Form");

// Método 2: MessageBox con traducciones
MessageBox.Show(
    LanguageBLL.Translate("Mensaje_De_Error"),
    LanguageBLL.Translate("Error"),
    MessageBoxButtons.OK,
    MessageBoxIcon.Error
);

// Método 3: Labels y controles
lblTitulo.Text = LanguageBLL.Translate("Etiqueta_Titulo");
```

### Traducción automática con BaseForm:

Los formularios que heredan de `BaseForm` **traducen automáticamente** sus controles en el evento `Load`:

```csharp
public partial class MiFormulario : BaseForm
{
    // Los controles se traducen automáticamente
    // Usa el Tag del control o su Text como clave
}
```

## 🔄 Cambiar Idioma en Tiempo de Ejecución

```csharp
// Cambiar idioma
LanguageBLL.SetLanguage("en-US");  // Inglés
LanguageBLL.SetLanguage("es-ES");  // Español

// Obtener idioma actual
string currentLang = LanguageBLL.GetCurrentLanguage();

// Refrescar traducciones en un formulario
this.RefreshLanguage();
```

## 🛠️ Generador Automático de Claves

### Script PowerShell: `GenerarClavesToaducciones.ps1`

Este script **extrae automáticamente** todas las claves usadas en el código y las agrega a los archivos de idioma.

### ¿Cómo usarlo?

1. **Ejecutar el script:**
   ```powershell
   .\GenerarClavesTraducciones.ps1
   ```

2. **El script:**
   - Busca todos los archivos `.cs` del proyecto
   - Extrae todas las llamadas a `LanguageBLL.Translate("clave")`
   - Identifica claves faltantes en los archivos `.lang`
   - Agrega automáticamente las claves faltantes
   - Genera un reporte de claves agregadas

3. **Resultado:**
   - Claves agregadas al final de `es-ES.lang`
   - Claves agregadas al final de `en-US.lang`
   - Reporte generado: `REPORTE_CLAVES_yyyyMMdd_HHmmss.txt`

### ⚠️ Importante:
- El script agrega las claves con un valor **provisional** (el nombre de la clave con espacios)
- **Debes revisar y completar las traducciones manualmente** después de ejecutar el script

### Ejemplo de salida del script:

```
================================================
 Generador de Claves de Traducción - TicketPro
================================================

[INFO] Buscando archivos .cs en el proyecto...
[INFO] Encontrados 156 archivos .cs

[INFO] Extrayendo claves de traducción...
[INFO] Total de claves únicas encontradas: 247

[INFO] Leyendo claves existentes en es-ES.lang...
[INFO] Claves existentes en es-ES.lang: 180

[WARNING] Claves faltantes: 67

[INFO] Agregando claves faltantes a es-ES.lang...
  + Nueva_Clave_1
  + Nueva_Clave_2
  ...

================================================
 [SUCCESS] Proceso completado
================================================

Claves agregadas: 67
Archivo español: C:\...\TicketPro\I18n\es-ES.lang
Archivo inglés:  C:\...\TicketPro\I18n\en-US.lang
Reporte:         C:\...\TicketPro\I18n\REPORTE_CLAVES_20260221_143022.txt
```

## 📝 Mantenimiento Manual

### Agregar una nueva traducción:

1. **Agregar en es-ES.lang:**
   ```
   Nueva_Clave=Nueva Traducción en Español
   ```

2. **Agregar en en-US.lang:**
   ```
   Nueva_Clave=New Translation in English
   ```

3. **Usar en el código:**
   ```csharp
   string texto = LanguageBLL.Translate("Nueva_Clave");
   ```

### ✅ Buenas Prácticas:

1. **Nombres de claves descriptivos:**
   - ✅ `Ticket_Error_Titulo_Vacio`
   - ❌ `Error1`

2. **Agrupar claves relacionadas:**
   ```
   # --- TICKETS ---
   Ticket_Label=Ticket
   Ticket_Crear=Crear Ticket
   Ticket_Editar=Editar Ticket
   ```

3. **Usar prefijos para módulos:**
   - `Inventario_*` para módulo de inventario
   - `Ticket_*` para módulo de tickets
   - `Cliente_*` para vista de clientes

4. **Mantener consistencia:**
   - Si usas `Error_Cargar_Datos`, usa `Error_Guardar_Datos` (no `Guardar_Error`)

## 🔍 Verificar Claves Faltantes

Si ves en los logs algo como:

```
[LANG] Clave de idioma faltante: Mi_Clave_Nueva
```

Significa que esa clave está siendo usada en el código pero NO existe en el archivo `.lang` actual.

**Solución:**
1. Ejecutar `GenerarClavesTraducciones.ps1`
2. O agregar manualmente la clave a ambos archivos `.lang`

## 🌐 Agregar un Nuevo Idioma

Para agregar soporte para un nuevo idioma (ej: francés):

1. **Crear archivo `fr-FR.lang`** en este directorio
2. **Copiar contenido** de `es-ES.lang` o `en-US.lang`
3. **Traducir todos los valores** al nuevo idioma
4. **Establecer el idioma:**
   ```csharp
   LanguageBLL.SetLanguage("fr-FR");
   ```

## 📊 Estadísticas Actuales

| Idioma | Archivo | Claves | Estado |
|--------|---------|--------|--------|
| Español | `es-ES.lang` | ~180 | ✅ Completo |
| Inglés | `en-US.lang` | ~180 | ✅ Completo |

## 🆘 Solución de Problemas

### Problema: "Archivo de idioma faltante"
**Solución:** Verificar que existe `TicketPro\I18n\es-ES.lang` y `en-US.lang`

### Problema: "Clave no encontrada"
**Solución:** Ejecutar el script `GenerarClavesTraducciones.ps1` para agregar claves faltantes

### Problema: "Traducciones no se aplican"
**Solución:**
1. Verificar que el formulario herede de `BaseForm`
2. Llamar a `RefreshLanguage()` después de cambiar el idioma
3. Verificar que el archivo `.lang` tenga codificación UTF-8

### Problema: Caracteres especiales se ven mal (ñ, á, é, etc.)
**Solución:** Los archivos `.lang` deben guardarse con codificación **UTF-8**

---

📌 **Última actualización:** 2026-02-21
🔧 **Versión del sistema de i18n:** 2.0
