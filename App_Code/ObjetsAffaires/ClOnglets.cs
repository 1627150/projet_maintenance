using System;

namespace CS2013.App_Code.ObjetsAffaires
{
    public class ClOnglets : ClObjet
    {
        public override void Valider()
        {
            throw new NotImplementedException();
        }

        public ClOnglets(int p_id, string p_titre, bool p_valid)
        {
            id = p_id;
            titre = p_titre;
            valid = p_valid;
        }

        public string titre
        {
            get;
            set;
        }

        public int id
        {
            get;
            set;
        }

        public bool valid
        {
            get;
            set;
        }
    }

}