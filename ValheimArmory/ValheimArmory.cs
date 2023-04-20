﻿// ValheimArmory
//  Adding tidbits to Valheim
// File:    ValheimArmory.cs
// Project: ValheimArmory

using BepInEx;
using Jotunn.Managers;
using Jotunn.Utils;
using System;
using Logger = Jotunn.Logger;
using UnityEngine;
using Jotunn.Entities;
using Jotunn.Configs;
using System.Collections.Generic;
using BepInEx.Configuration;

namespace ValheimArmory
{
    [BepInPlugin(PluginGUID, PluginName, PluginVersion)]
    [BepInDependency(Jotunn.Main.ModGuid)]
    [NetworkCompatibility(CompatibilityLevel.EveryoneMustHaveMod, VersionStrictness.Minor)]
    internal class ValheimArmory : BaseUnityPlugin
    {
        public const string PluginGUID = "com.midnightsfx.ValheimArmory";
        public const string PluginName = "ValheimArmory";
        public const string PluginVersion = "1.2.0";

        AssetBundle EmbeddedResourceBundle;
        CustomLocalization Localization;


        private void Awake()
        {
            // build the config class, and ensure defaults are available / configs ingested.
            VAConfig cfg = new VAConfig(Config);

            // Load assets
            LoadAssets(cfg);

            // Build the piece & item creation classes, provide configuration for toggles and loaded resources
            new ValheimArmoryPieces(EmbeddedResourceBundle, cfg); // not used right now
            new ValheimArmoryItems(EmbeddedResourceBundle, cfg);

            AddLocalizations();
            UnloadAssets();
        }


        private void AddLocalizations()
        {
            Localization = new CustomLocalization();
            LocalizationManager.Instance.AddLocalization(Localization);

            // Add translations for our custom items
            Localization.AddTranslation("English", new Dictionary<string, string>
            {   
                // Arrows
                {"item_arrow_greenmetal", "Blackmetal Arrow"}, {"item_arrow_greenmetal_description", "A piercing darkness, may your aim be true."},
                {"item_bone_arrow", "Bone Arrow"}, {"item_bone_arrow_description", "Just giving a greydwarf a bone."},
                {"item_arrow_surtlingfire", "Surtling Fire Arrow"}, {"item_arrow_surtlingfire_description", "This does not seem safe, hopefully more so for what you are aiming at."},
                {"item_ancient_arrow", "Ancient Wood Arrow"}, {"item_ancient_arrow_description", "Looks like it will splinter at any given time."},
                {"item_arrow_chitin", "Chitin Arrow"}, {"item_arrow_chitin_description", "Not as sharp as other arrows but it causes a nasty cut regardless."},
                {"item_bolt_wood", "Wood Bolt"}, {"item_bolt_wood_description", "A little more than a pointy stick. Shot hard enough, it sure hurts!"},
                // Bows
                {"item_crossbow_bronze", "Bronze Crossbow"}, {"item_crossbow_bronze_description", "Not perfect, but a weapon like this should be able to hurl bolts at incredible speeds. Not very durable."},
                // Swords
                {"item_sword_chitin", "Abyssal Sword"}, {"item_sword_chitin_description", "It may not be the sharpest but with enough force it still hurts, a lot."},
                // Shields
                {"item_serpent_buckler", "Serpent Scaled Buckler"}, {"item_serpent_buckler_description", "A flexible wooden-iron woven shield fronted by an array of shiny scales."},
                // Atgeirs
                {"item_atgeir_chitin_heavy", "Royal Abyssal Atgeir"}, {"item_atgeir_chitin_heavy_description", "A sharpened Chitin Atgeir with a silvercore, fit for the bravest of vikings."},
                // Hammers
                {"item_sledge_blackmetal", "Sky Shatter"}, {"item_sledge_blackmetal_description", "Heavy chunks of blackmetal fused with thunderstones. Causes lightning strikes."},
                // Axes
                {"item_battleaxe_bronze", "Timber Axe"}, {"item_battleaxe_bronze_description", "An especially large bronze axe, great for removing heads and trees."},
                {"item_battleaxe_blackmetal", "Herkir's Wrath"}, {"item_battleaxe_blackmetal_description", "Giant Killer, fire bringer, pain maker."},
                // Greatswords
                {"item_bronze_greatsword", "Bronze Greatsword"}, {"item_bronze_greatsword_description", "A massive bronze blade handled with corewood, not very durable but quite sharp."},
                {"item_iron_greatsword", "Iron Greatsword"}, {"item_iron_greatsword_description", "A huge rough iron blade with a hewn stone hilt. With a big enough blade, anything dies."},
                {"item_silver_greatsword", "Silver Runic Greatsword"}, {"item_silver_greatsword_description", "Silver forged with wrought iron, emblazoned with runes to ward off the undead."},
                // Daggers
                {"item_dagger_iron", "Iron Dagger"}, {"item_dagger_iron_description", "Not as sharp as the Abyssal knife, but it cuts nicely."},
                {"item_dagger_iron_2h", "Rouge Daggers"}, {"item_dagger_iron_2h_description", "Two Iron knives makes you one average viking rouge."},
                {"item_dagger_bronze", "Bronze Dagger"}, {"item_dagger_bronze_description", "Sharper than copper, its a piercing stab."},
                {"item_dagger_bronze_2h", "Rascal Daggers"}, {"item_dagger_bronze_description_2h", "Now you just need to start leveling your pickpocket skill."},
                {"item_dagger_silver", "Silver Dagger"}, {"item_dagger_silver_description", "A Sharp silver short blade etched with runes."},
                {"item_dagger_silver_2h", "Blackguard Runic Daggers"}, {"item_dagger_silver_2h_description", "Men and beast alike fear the slice and dice."},
                // Boss Weapons | Eikthyrs
                {"item_bow_antler", "Eikthyrs Herald" }, {"item_bow_antler_description", "Herald of lightning, may all hear its thunder."},
                {"item_dagger_antler", "Eikthyrs Spike" }, {"item_dagger_antler_description", "A sharpened shard from Eikthyrs antlers, still teaming with electricity."},
                {"item_sword_antler", "Eikthyrs Rage" }, {"item_sword_antler_description", "Twisted Antlers bound by chains from Eikthyrs remains, still seething with energy."},
                {"item_atgeir_antler", "Eikthyrs Charge"}, {"item_atgeir_antler_description", "A Branch from Eikthyrs crown still sparking attached to a pole, the Antlergeir."},
                {"item_battleaxe_antler", "Eikthyrs Crown"}, {"item_battleaxe_antler_description", "Twin Antlers sharpened and chained, they still spark with Eikthyrs life."},
            });

            Localization.AddTranslation("German", new Dictionary<string, string>
            {   
                // Arrows
                {"item_arrow_greenmetal", "Schwarzmetallpfeil"}, {"item_arrow_greenmetal_description", "Eine durchdringende Dunkelheit, möge dein Ziel wahr sein."},
                {"item_bone_arrow", "Knochenpfeil"}, {"item_bone_arrow_description", "Ich gebe nur einem Grauzwerg einen Knochen."},
                {"item_arrow_surtlingfire", "Surtling Feuerpfeil"}, {"item_arrow_surtlingfire_description", "Dies scheint nicht sicher zu sein, hoffentlich noch mehr für das angestrebte Ziel."},
                {"item_ancient_arrow", "Uralter Holzpfeil"}, {"item_ancient_arrow_description", "Sieht so aus, als würde es jederzeit splittern."},
                {"item_arrow_chitin", "Chitin-Pfeil"}, {"item_arrow_chitin_description", "Nicht so scharf wie andere Pfeile, aber es verursacht trotzdem einen bösen Schnitt."},
                {"item_bolt_wood", "Holzbolzen"}, {"item_bolt_wood_description", "Etwas mehr als ein spitzer Stock. Hart genug geschossen, tut es sicher weh!"},
                // Bows
                {"item_crossbow_bronze", "Bronzene Armbrust"}, {"item_crossbow_bronze_description", "Nicht perfekt, aber eine Waffe wie diese sollte in der Lage sein, Bolzen mit unglaublicher Geschwindigkeit zu schleudern. Nicht sehr langlebig."},
                // Swords
                {"item_sword_chitin", "Abgründiges Schwert"}, {"item_sword_chitin_description", "Es ist vielleicht nicht das schärfste, aber mit genug Kraft tut es immer noch sehr weh."},
                // Shields
                {"item_serpent_buckler", "Schlangenschuppiger Buckler"}, {"item_serpent_buckler_description", "Ein biegsamer, aus Holz und Eisen gewebter Schild mit einer Reihe glänzender Schuppen."},
                // Atgeirs
                {"item_atgeir_chitin_heavy", "Royal Abyssal Atgeir"}, {"item_atgeir_chitin_heavy_description", "Ein geschärfter Chitin-Atgeir mit einem Silberkern, geeignet für die tapfersten Wikinger."},
                // Hammers
                {"item_sledge_blackmetal", "Himmelsbruch"}, {"item_sledge_blackmetal_description", "Schwere Brocken aus schwarzem Metall, verschmolzen mit Donnersteinen. Verursacht Blitzeinschläge."},
                // Axes
                {"item_battleaxe_bronze", "Holz Axt"}, {"item_battleaxe_bronze_description", "Eine besonders große Bronzeaxt, ideal zum Entfernen von Köpfen und Bäumen."},
                {"item_battleaxe_blackmetal", "Herkirs Zorn"}, {"item_battleaxe_blackmetal_description", "Riesiger Killer, Feuerbringer, Schmerzmacher."},
                // Greatswords
                {"item_bronze_greatsword", "Bronzenes Großschwert"}, {"item_bronze_greatsword_description", "Eine massive Bronzeklinge, die mit Kernholz behandelt wird, nicht sehr haltbar, aber ziemlich scharf."},
                {"item_iron_greatsword", "Eisernes Großschwert"}, {"item_iron_greatsword_description", "Eine riesige raue Eisenklinge mit einem behauenen Steingriff. Mit einer ausreichend großen Klinge stirbt alles."},
                {"item_silver_greatsword", "Silbernes Runen-Großschwert"}, {"item_silver_greatsword_description", "Aus Schmiedeeisen geschmiedetes Silber, mit Runen geschmückt, um die Untoten abzuwehren."},
                // Daggers
                {"item_dagger_iron", "Eiserner Dolch"}, {"item_dagger_iron_description", "Nicht so scharf wie das Abyssal-Messer, aber es schneidet gut."},
                {"item_dagger_iron_2h", "Schurken Dolch"}, {"item_dagger_iron_2h_description", "Zwei Eisenmesser machen Sie zu einem durchschnittlichen Wikinger-Rouge."},
                {"item_dagger_bronze", "Bronzener Dolch"}, {"item_dagger_bronze_description", "Schärfer als Kupfer, es ist ein durchdringender Stich."},
                {"item_dagger_bronze_2h", "Schlingel Dolch"}, {"item_dagger_bronze_description_2h", "Jetzt muss man nur noch damit beginnen, seine Taschendiebstahlfähigkeiten zu verbessern."},
                {"item_dagger_silver", "Silberner Dolch"}, {"item_dagger_silver_description", "Eine scharfe silberne kurze Klinge, die mit Runen geätzt ist."},
                {"item_dagger_silver_2h", "Schwarzwächter-Runendolche"}, {"item_dagger_silver_2h_description", "Menschen und Bestien fürchten gleichermaßen die Scheiben und Würfel."},
                // Boss Weapons | Eikthyrs
                {"item_bow_antler", "Eikthyrs Herald" }, {"item_bow_antler_description", "Herald of lightning, may all hear its thunder."},
                {"item_dagger_antler", "Eikthyrs Spike" }, {"item_dagger_antler_description", "A sharpened shard from Eikthyrs antlers, still teaming with electricity."},
                {"item_sword_antler", "Eikthyrs Rage" }, {"item_sword_antler_description", "Twisted Antlers bound by chains from Eikthyrs remains, still seething with energy."},
                {"item_atgeir_antler", "Eikthyrs Charge"}, {"item_atgeir_antler_description", "A Branch from Eikthyrs crown still sparking attached to a pole, the Antlergeir."},
                {"item_battleaxe_antler", "Eikthyrs Crown"}, {"item_battleaxe_antler_description", "Twin Antlers sharpened and chained, they still spark with Eikthyrs life."},
            });
        }

        private void LoadAssets(VAConfig cfg)
        {
            if (cfg.EnableDebugMode.Value == true)
            {
                Logger.LogInfo($"Embedded resources: {string.Join(",", typeof(ValheimArmory).Assembly.GetManifestResourceNames())}");
            }
            EmbeddedResourceBundle = AssetUtils.LoadAssetBundleFromResources("ValheimArmory.AssetsEmbedded.vabundle", typeof(ValheimArmory).Assembly);

            if (cfg.EnableDebugMode.Value == true)
            {
                Logger.LogInfo($"Asset Names: {string.Join(",", EmbeddedResourceBundle.GetAllAssetNames())}");
            }
        }

        private void UnloadAssets()
        {
            EmbeddedResourceBundle.Unload(false);
        }

    }
}