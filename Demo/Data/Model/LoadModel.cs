using Demo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    class LoadModel
    {
        #region Load Model
        private const String APP_SETTINGS_FILE_NAME = "app_settings.json";
        public static AppSettings appSettings = new AppSettings();
        public static ModelSettings currentModel = new ModelSettings();
        public static void StartUp()
        {
            loadAppSettings(APP_SETTINGS_FILE_NAME);
            if (appSettings == null)
            {
                appSettings = new AppSettings();
            }
            currentModel = ModelStore.GetModelSettings(appSettings.currentModel);
            if (currentModel == null)
            {
                currentModel = new ModelSettings();
            }
        }
        public static void loadAppSettings(String fileName)
        {
            try
            {
                String filePath = System.IO.Path.Combine(Directory.GetCurrentDirectory(), fileName);
                if (File.Exists(filePath))
                {
                    using (StreamReader file = File.OpenText(filePath))
                    {
                        appSettings = AppSettings.FromJSON(file.ReadToEnd());
                    }
                }
                else
                {
                    SaveAppSettings();
                    appSettings = new AppSettings();
                }
            }
            catch (Exception ex)
            {

            }
        }

        public static void SaveAppSettings()
        {
            try
            {
                String filePath = Path.Combine(Directory.GetCurrentDirectory(), APP_SETTINGS_FILE_NAME);
                var js = appSettings.ToJSON();
                File.WriteAllText(filePath, js);
            }
            catch (Exception ex)
            {

            }
        }




        public static void SaveCurrentModelSettings()
        {
            ModelStore.UpdateModelSettings(currentModel);
        }

        public ModelSettings GetCurrentModelSettings()
        {
            return currentModel;
        }

        public static void ReplaceModel(ModelSettings model)
        {
            try
            {
                if (model != null)
                {
                    currentModel = model;
                    appSettings.currentModel = model.modelName;
                    // Save appSettings:
                    SaveAppSettings();
                }
            }
            catch (Exception ex)
            {

            }
        }
        #endregion
    }
}
