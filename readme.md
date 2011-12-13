Visual Studio Plugin to perform convention based bulk updates to EDMX files.

I generated classes for the EDMX XSDs using **XSD.EXE**:

    xsd.exe Microsoft.Data.Entity.Design.Edmx_2.xsd System.Data.Resources.CSDLSchema_2.xsd System.Data.Resources.CSMSL_2.xsd System.Data.Resources.SSDLSchema_2.xsd Microsoft.Data.Entity.Design.Edmx_2.xsd System.Data.Resources.CodeGenerationSchema.xsd System.Data.Resources.AnnotationSchema.xsd .\System.Data.Resources.EntityStoreSchemaGenerator.xsd /classes /n:EdmxUpdater.Schemas

*Note the ".\" on the last XSD path, this is a workaround for output file name being too long*

And perform iteration over the entity types to update.