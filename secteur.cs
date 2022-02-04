using System;

namespace refactoring
{
    public class Secteur{

        Random rand = new Random();
        /*
        * generer un PRNG entre Min & Max
        * generer un secteur
        * generer un crash
        * generer un arret au stand
        */

        /**
        * nombre generer de maniere pseudo aleatoire
        * ATTENTION : il faut utiliser une seed sur rand() prealablement pour eviter que
        * les voitures sortent toujours les memes nombres au meme moment
        *
        * @param int min
        * @param int max
        * NB : min doit etre plus grand que max
        *
        * @return int le nombre aleatoire
        */
        public int my_rand(int min, int max){
            int c = rand.Next(max-min+1)+min;//creation du nombre aleatoire
            return c;
        }
        public int secteur(int taille1, int taille2, int chance){

            int temp = 0;

            if(!crash(chance)){//si il n'y a pas de crash
                temp = my_rand(taille1, taille2);//Generation du nombre aleatoire
                return temp;
            }
            return temp;//si il y a un crash
        }
        /** permet de savoir si la voiture c'est crashee
        *
        * @param int chance la probabilite d'avoir un crash
        * return true si il est plus petit que 49 sinon false
        *
        * @return bool retourne true si il y a crash, sinon false
        */
        public bool crash(int chance){//methode pour determiner si il y a un crash
            if(my_rand(1,1000000) <= chance){
                    return true;
        }
        return false;
        }

        /** permet de savoir si la voiture doit aller au stand
        *
        * @param int chance   la probabilite d'avoir un crash
        *
        * @return bool retourne true si il y a crash, sinon false
        *
        */
        public bool stand(){//passage au stand
            if(my_rand(1,20) <= 2){
                return true;
            }
            return false;

        }

    }  
}

