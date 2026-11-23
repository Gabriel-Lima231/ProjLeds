using System;

namespace ProjLeds
{
    internal class Leds
    {
        private byte status;

        public Leds()
        {
            this.status = 0;
        }

        public Leds(byte status)
        {
            this.status = status;
        }

        
        private int Mascara(int led)
        {
            return (int)Math.Pow(2, led - 1);
        }

        public void acender(int led)
        {
            if (led < 1 || led > 8) return;

            int dado = this.status;              
            dado = dado | Mascara(led);          
            this.status = (byte)dado;
        }

        public void apagar(int led)
        {
            if (led < 1 || led > 8) return;

            int dado = this.status;
            dado = dado & (255 - Mascara(led));  
            this.status = (byte)dado;
        }

        public byte getStatus()
        {
            return this.status;
        }

        public bool getStatus(int led)
        {
            if (led < 1 || led > 8) return false;

            return (this.status & Mascara(led)) != 0;
        }
    }
}