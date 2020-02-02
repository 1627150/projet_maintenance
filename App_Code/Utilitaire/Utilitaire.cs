using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Hosting;
using System.Web.SessionState;
using System.Web.UI.WebControls;

namespace CS2013
{
    class Utilitaire
    {
        private static string Key
        {
            get
            {
                return "CreditsSante";
            }
        }

        /// <summary>
        /// Prend tout les labels dans controlCollection et vérifie si leur ID est égale au nom de la clé du dictionnaire.
        /// Si c'est le cas, on remplace la valeur de son texte par le message d'erreur
        /// </summary>
        /// <param name="p_tableauErreur">Le tableau contenant le nom du label à remplir et le message à mettre dedans.</param>
        /// <param name="p_panel">La liste de tous les controles du "Panel" asp</param>
        public static void RemplirLabelDErreurs(Dictionary<string, string> p_tableauErreur, Panel p_panel)
        {
            foreach (KeyValuePair<string, string> élément in p_tableauErreur)
            {
                Label labelCourrant = (Label)p_panel.FindControl(élément.Key);
                if (labelCourrant != null)
                {
                    labelCourrant.Text = élément.Value;
                }
            }

            p_tableauErreur.Clear();
        }

        /// <summary>
        /// Converti l'intensité reçu en paramètre pour avoir un affichage correcte rendu à l'interface
        /// </summary>
        /// <param name="p_intensite">L'intensité à convertir</param>
        /// <returns>L'intensité en string bien formatté</returns>
        public static string ConvertirIntensite(int p_intensite)
        {
            switch (p_intensite)
            {
                case 1:
                    return "Faible";
                case 2:
                    return "Modérée";
                case 3:
                    return "Élevé";
                default:
                    return null;
            }
        }

        public static string CreditsEnTexte(double p_nbCredits)
        {
            string creditsConvertis;
            if (Convert.ToInt32(p_nbCredits) == p_nbCredits)
                creditsConvertis = String.Format("{0:0.##}", p_nbCredits);
            else
                creditsConvertis = String.Format("{0:0.00}", p_nbCredits);

            return creditsConvertis.Replace('.', ',');
        }

        public static TypeUser GetTypeUser(int user)
        {
            if (user == (int)TypeUser.CegepEmploye)
                return TypeUser.CegepEmploye;
            else if (user == (int)TypeUser.CegepEtudiant)
                return TypeUser.CegepEtudiant;
            else if (user == (int)TypeUser.EtudiantExterne)
                return TypeUser.EtudiantExterne;
            else if (user == (int)TypeUser.Partenaire)
                return TypeUser.Partenaire;
            else if (user == (int)TypeUser.Communaute)
                return TypeUser.Communaute;
            else
                return TypeUser.Invalide;

        }

        public static string GetTypeUserString(int noTypeUser)
        {
            return GetTypeUserString(GetTypeUser(noTypeUser));
        }

        public static string GetTypeUserString(TypeUser user)
        {
            if (user == TypeUser.CegepEmploye)
                return "Personnel du Cégep Saint-Jean-sur-Richelieu";
            else if (user == TypeUser.CegepEtudiant)
                return "Étudiante ou étudiant du Cégep Saint-Jean-sur-Richelieu";
            else if (user == TypeUser.EtudiantExterne)
                return "Étudiante ou étudiant du Québec";
            else if (user == TypeUser.Partenaire)
                return "Partenaire du Cégep Saint-Jean-sur-Richelieu";
            else if (user == TypeUser.Communaute)
                return "Communauté extérieure au Cégep";
            else
                return String.Empty;
        }

        public static string GetTypeConcentrationString(TypeConcentration type)
        {
            if (type == TypeConcentration.Communaute)
                return "Communauté extérieure au Cégep";
            else if (type == TypeConcentration.Département)
                return "Personnel du Cégep Saint-Jean-sur-Richelieu";
            else if (type == TypeConcentration.Ecole)
                return "Étudiante ou étudiant du Québec";
            else if (type == TypeConcentration.Partenaire)
                return "Partenaire du Cégep Saint-Jean-sur-Richelieu";
            else if (type == TypeConcentration.ProgrammeAccueilIntegration)
                return "Étudiante ou étudiant du Cégep (programme accueil et intégration)";
            else if (type == TypeConcentration.ProgrammePreUniversitaire)
                return "Étudiante ou étudiant du Cégep (programmes pré-universitaire)";
            else if (type == TypeConcentration.ProgrammeTechnique)
                return "Étudiante ou étudiant du Cégep (programmes technique)";
            else if (type == TypeConcentration.Service)
                return "Services du Cégep";
            else
                return String.Empty;
        }

        public static void FillDropDownTypeUser(DropDownList dropDown)
        {
            foreach (TypeUser type in Enum.GetValues(typeof(TypeUser)))
            {
                if (type == TypeUser.Invalide)
                    continue;

                ListItem item = new ListItem(Utilitaire.GetTypeUserString(type), Convert.ToString((int)type));
                dropDown.Items.Add(item);
            }
        }

        public static void RemplirComboFournisseur(DropDownList dropFournisseur)
        {
            dropFournisseur.Items.Clear();

            string fournisseurs = ClCourtierParametre.GetInstance().ObtenirSelonNom(ClParametre.TypeParametre.FournisseurServiceCell.ToString()).Value;

            string[] listeFournisseurs = fournisseurs.Split('|');
            Array.Sort(listeFournisseurs);

            ListItem item = new ListItem("Choisir un fournisseur", "");
            dropFournisseur.Items.Add(item);

            foreach (string fournisseur in listeFournisseurs)
            {
                string[] infos = fournisseur.Split(':');
                item = new ListItem(infos[0], infos[1]);

                dropFournisseur.Items.Add(item);
            }
        }

        public static void FillDropDownTypeConcentration(DropDownList dropDown)
        {
            foreach (TypeConcentration type in Enum.GetValues(typeof(TypeConcentration)))
            {
                if (type == TypeConcentration.NULL)
                    continue;

                ListItem item = new ListItem(Utilitaire.GetTypeConcentrationString(type), type.ToString());
                dropDown.Items.Add(item);
            }
        }

        public static string Encrypt(string ToEncrypt, bool useHasing)
        {
            byte[] keyArray;
            byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(ToEncrypt);

            if (useHasing)
            {
                MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(Key));
                hashmd5.Clear();
            }
            else
            {
                keyArray = UTF8Encoding.UTF8.GetBytes(Key);
            }
            TripleDESCryptoServiceProvider tDes = new TripleDESCryptoServiceProvider();
            tDes.Key = keyArray;
            tDes.Mode = CipherMode.ECB;
            tDes.Padding = PaddingMode.PKCS7;
            ICryptoTransform cTransform = tDes.CreateEncryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
            tDes.Clear();
            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }

        public static string Decrypt(string cypherString, bool useHasing)
        {
            byte[] keyArray;
            byte[] toDecryptArray = Convert.FromBase64String(cypherString);

            if (useHasing)
            {
                MD5CryptoServiceProvider hashmd = new MD5CryptoServiceProvider();
                keyArray = hashmd.ComputeHash(UTF8Encoding.UTF8.GetBytes(Key));
                hashmd.Clear();
            }
            else
            {
                keyArray = UTF8Encoding.UTF8.GetBytes(Key);
            }
            TripleDESCryptoServiceProvider tDes = new TripleDESCryptoServiceProvider();
            tDes.Key = keyArray;
            tDes.Mode = CipherMode.ECB;
            tDes.Padding = PaddingMode.PKCS7;
            ICryptoTransform cTransform = tDes.CreateDecryptor();
            try
            {
                byte[] resultArray = cTransform.TransformFinalBlock(toDecryptArray, 0, toDecryptArray.Length);

                tDes.Clear();
                return UTF8Encoding.UTF8.GetString(resultArray, 0, resultArray.Length);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void SendEMail()
        {
        }

        public static List<List<T>> Chunk<T>(
        List<T> theList,
        int chunkSize
    )
        {
            List<List<T>> result = theList
                .Select((x, i) => new
                {
                    data = x,
                    indexgroup = i / chunkSize
                })
                .GroupBy(x => x.indexgroup, x => x.data)
                .Select(g => new List<T>(g))
                .ToList();

            return result;
        }

        public static string ConcentrationTexteSelonType(TypeConcentration concentration)
        {
            switch (concentration)
            {
                case TypeConcentration.Département:
                    return "Département";
                case TypeConcentration.ProgrammePreUniversitaire:
                    return "Programme Pré-universitaire";
                case TypeConcentration.Service:
                    return "Service";
                case TypeConcentration.ProgrammeTechnique:
                    return "Programme technique";
                case TypeConcentration.ProgrammeAccueilIntegration:
                    return "Programme accueil";
                case TypeConcentration.Partenaire:
                    return "Partenaire du cégep";
                case TypeConcentration.Ecole:
                    return "Scolaire externe";
                case TypeConcentration.Communaute:
                    return "Communauté";
                default:
                    return String.Empty;
            }
        }

        public static bool AffecterParametres(HttpSessionState session)
        {
            if (session.Keys.Count > 2)
                return true;

            List<ClParametre> listParams = ClCourtierParametre.GetInstance().ObtenirTous();

            if (listParams == null)
                return false;

            foreach (ClParametre param in listParams)
            {
                if (param.Date.CompareTo(new DateTime(1, 1, 1)) != 0)
                    session[param.Nom] = param.Date;
                else if (param.Value == "")
                    session[param.Nom] = param.Value;
                else
                    session[param.Nom] = param.Valeur.ToString();
            }

            return true;
        }

        
       // Gestion de fichiers
        
        public static Exception TeleverserImage(HttpPostedFile p_fichier, string p_repertoire, string p_nomFichier, bool p_estNomUnique)
        {
            if (string.IsNullOrEmpty(p_nomFichier))
            {
                p_nomFichier = p_fichier.FileName;
            }

            try
            {
                string path = Path.GetFileName(p_fichier.FileName);
                if (!EstFichierJpeg(path))
                {
                    throw new NotSupportedException("Seuls les fichiers JPEG sont autorisés.");
                }

                string extension = GetExtensionFichier(path).ToLower();
                path = Path.Combine(HostingEnvironment.ApplicationPhysicalPath, p_repertoire, Path.GetFileNameWithoutExtension(p_nomFichier));   // Fichier sans extension

                if (p_estNomUnique)
                {
                    if (File.Exists(path + ".jpg"))
                    {
                        File.Delete(path + ".jpg");
                    }                    
                }

                path += extension;   // Fichier avec extension

                p_fichier.SaveAs(path);
                return null;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        public static string RepertoireAbsoluARelatif(string p_cheminFichier, string p_cheminReference)
        {
            var uriFichier = new Uri(p_cheminFichier);
            var uriReference = new Uri(p_cheminReference);
            return "/" + uriReference.MakeRelativeUri(uriFichier).ToString();
        }

        public static List<string> ListerImages(string p_repertoire)
        {
            List<string> images = new List<string>();   // Fichiers d'images
            string[] fichiers = Directory.GetFiles(p_repertoire);   // Tous les fichiers du répertoire

            foreach (string fichier in fichiers)
            {
                int i = 0;
                if (EstFichierJpeg(fichier) || EstFichierPng(fichier))
                {
                    images.Add(fichier);
                }
            }

            return images;
        }

        public static string GetExtensionFichier(string p_fichier)
        {
            return System.IO.Path.GetExtension(p_fichier);
        }

        public static bool EstFichierJpeg(string p_fichier)
        {
            string extension = GetExtensionFichier(p_fichier);
            return extension == ".jpg" || extension == ".JPG";
        }

        public static bool EstFichierPng(string p_fichier)
        {
            string extension = GetExtensionFichier(p_fichier);
            return extension == ".png" || extension == ".PNG";
        }

        #region Configurations
        /// <summary>
        /// method to retrieve connection stringed in the web.config file
        /// </summary>
        /// <param name="str">Name of the connection</param>
        /// <remarks>Need a reference to the System.Configuration Namespace</remarks>
        /// <returns></returns>
        public static string GetConnectionString(string str)
        {
            //variable to hold our return value
            string conn = string.Empty;
            //check if a value was provided
            if (!string.IsNullOrEmpty(str))
            {
                //name provided so search for that connection
                conn = ConfigurationManager.ConnectionStrings[str].ConnectionString;
            }
            else
            //name not provided, get the 'default' connection
            {
                conn = ConfigurationManager.ConnectionStrings["DonneesCreditsSante"].ConnectionString;
            }
            // return the value
            // return conn; 
            // TODO:CHANGER URL STRING POUET POUET POUET
            // return "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\CreditSante.mdf;Integrated Security=True";
            return "Data Source = (localdb)\\MSSQLLocalDB; Initial Catalog = CreditSante; Integrated Security = True; Connect Timeout = 30; Encrypt = False; TrustServerCertificate = False; ApplicationIntent = ReadWrite; MultiSubnetFailover = False";
        }

        public static string GetAdminEmail()
        {
            return ConfigurationManager.AppSettings["mailSettings"];
        }
        #endregion
    }
}
