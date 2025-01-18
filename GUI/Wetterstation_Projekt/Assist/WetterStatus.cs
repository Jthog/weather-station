using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wetterstation_Projekt.Model;
using Wetterstation_Projekt.ViewModel;

namespace Wetterstation_Projekt.Assist
{
    public class WetterStatus
    {
        public static string GetWetterStatus(bool istNacht, int statusCode) 
        {
            StringBuilder pfad = new StringBuilder("/Bilder/Wetter/");
            WetterStatusEnum code = (WetterStatusEnum)statusCode;

            if (code == WetterStatusEnum.KlarerHimmel)
            {
                if (istNacht)
                {
                    pfad.Append("mond.png");
                }
                else
                {
                    pfad.Append("sonnig.png");
                }

            }
            else if (code == WetterStatusEnum.LeichtBewoelkt)
            {
                if (istNacht)
                {
                    pfad.Append("nachtBewoelkt.png");
                }
                else
                {
                    pfad.Append("leichtBewoelkt.png");
                }
            }
            else if (code == WetterStatusEnum.Bewoelkt)
            {
                if (istNacht)
                {
                    pfad.Append("nachtBewoelkt.png");
                }
                else
                {
                    pfad.Append("bewoelkt.png");
                }
            }
            else if (code == WetterStatusEnum.Bedeckt)
            {
                pfad.Append("bedeckt.png");
            }
            else if (code == WetterStatusEnum.StellenweiseRegelfall || code == WetterStatusEnum.ZuweilenMaessigerRegenfall)
            {
                pfad.Append("stellenweiseRegen.png");
            }
            else if (code == WetterStatusEnum.StellenweiseSchneefall || code == WetterStatusEnum.StellenweiseMaessigerSchneefall ||
                     code == WetterStatusEnum.MaessigerSchneefall || code == WetterStatusEnum.MaessigerStarkeSchneegestoeber)
            {
                if (istNacht)
                {
                    pfad.Append("nachtSchneefall.png");
                }
                else
                {
                    pfad.Append("Schneefall.png");
                }
            }
            else if (code == WetterStatusEnum.StellenweiseLeichterSchneefall || code == WetterStatusEnum.LeichterSchneefall || code == WetterStatusEnum.LeichterSchneegestoeber ||
                     code == WetterStatusEnum.StellenweiseGefrorenderNieselregen || code == WetterStatusEnum.GefrorenderNieselregen)
            {
                if (istNacht)
                {
                    pfad.Append("nachtLeichterSchneefall.png");
                }
                else
                {
                    pfad.Append("leichterSchneefall.png");
                }
            }
            else if (code == WetterStatusEnum.LeichterEisregen || code == WetterStatusEnum.StellenweiseEisregen || code == WetterStatusEnum.MaessigerStarkerEisregen)
            {
                pfad.Append("Eisregene.png");
            }
            else if (code == WetterStatusEnum.GewittrigeNiederschläge || code == WetterStatusEnum.StellenweiseLeichterRegenfallGewitter || code == WetterStatusEnum.MaessigerStarkerRegenfallGewitter ||
                     code == WetterStatusEnum.StellenweiseLeichterSchneefallGewitter || code == WetterStatusEnum.MaessigerStarkerSchneefallGewitter)
            {
                pfad.Append("regenGewitter.png");
            }
            else if (code == WetterStatusEnum.Schneesturm || code == WetterStatusEnum.StarkerSchneefall || code == WetterStatusEnum.StellenweiseStarkerSchneefall)
            {
                if (istNacht)
                {
                    pfad.Append("nachtStarkerSchneefall.png");
                }
                else
                {
                    pfad.Append("starkerSchneefall.png");
                }
            }
            else if (code == WetterStatusEnum.Nebel || code == WetterStatusEnum.Eisnebel || code == WetterStatusEnum.LeichterNebel)
            {
                pfad.Append("nebelig.png");
            }
            else if (code == WetterStatusEnum.StellenweiseNieselregen || code == WetterStatusEnum.StellenweiseLeichterRegenfall)
            {
                pfad.Append("stellenweiseLeichterRegen.png");
            }
            else if (code == WetterStatusEnum.Nieselregen || code == WetterStatusEnum.LeichterRegenfall || code == WetterStatusEnum.LeichterRegenschauer)
            {
                if (istNacht)
                {
                    pfad.Append("nachtLeichterRegen.png");
                }
                else
                {
                    pfad.Append("leichterRegen.png");
                }
            }
            else if (code == WetterStatusEnum.StarkerNieseslregen || code == WetterStatusEnum.MaessigerRegenfall || code == WetterStatusEnum.MaessigerStarkerRegenschauer)
            {
                if (istNacht)
                {
                    pfad.Append("nachtRegen.png");
                }
                else
                {
                    pfad.Append("Regen.png");
                }
            }
            else if (code == WetterStatusEnum.ZuweilenStarkerRegenfall || code == WetterStatusEnum.StarkerRegenfall)
            {
                if (istNacht)
                {
                    pfad.Append("nachtStarkerRegen.png");
                }
                else
                {
                    pfad.Append("StarkerRegen.png");
                }
            }
            else if (code == WetterStatusEnum.LeichterGraupelschauer || code == WetterStatusEnum.MaessigerSchwererGraupelschauer)
            {
                pfad.Append("Schneeflocke.png");
            }

            return pfad.ToString();
        }
    }
}
