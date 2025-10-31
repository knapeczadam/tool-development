using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace W06_DestinyGearViewer.Models
{
    public enum BungieMembershipType : int
    {
        None = 0,
        Xbox = 1,
        Psn = 2,
        Demon = 10,
        BungieNext = 254,
        All = -1
    }

    public enum DamageTypes : int
    {
        None = 0,
        Kinetic = 1,
        Arc = 2,
        Thermal = 3,
        Void = 4,
        Raid = 5
    }

    public enum TierTypes : int
    {
        Unknown = 0,
        Currency = 1,
        Basic = 2,
        Common = 3,
        Rare = 4,
        Superior = 5,
        Exotic = 6
    }

    public enum ClassTypes : int
    {
        Titan = 0,
        Hunter = 1,
        Warlock = 2,
        Unknown = 3
    }

    public enum ItemTypes : int
    {
        None = 0,
        Currency = 1,
        Armor = 2,
        Weapon = 3,
        Bounty = 4,
        CompletedBounty = 5,
        BountyReward = 6,
        Message = 7,
        Engram = 8,
        Consumable = 9,
        ExchangeMaterial = 10,
        MissionReward = 11,
        QuestStep = 12,
        QuestStepComplete = 13,
        Emblem = 14,
        Quest = 15
    }

    public enum ItemSubTypes : int
    {
        None = 0,
        Crucible = 1,
        Vanguard = 2,
        IronBanner = 3,
        Queen = 4,
        Exotic = 5,
        AutoRifle = 6,
        Shotgun = 7,
        Machinegun = 8,
        HandCannon = 9,
        RocketLauncher = 10,
        FusionRifle = 11,
        SniperRifle = 12,
        PulseRifle = 13,
        ScoutRifle = 14,
        Camera = 15,
        Crm = 16,
        Sidearm = 17,
        Sword = 18,
        Mask = 19
    }

    public enum SpecialItemTypes : int
    {
        None = 0,
        SpecialCurrency = 1,
        CompletedBounty = 2,
        CrucibleBounty = 3,
        VanguardBounty = 4,
        IronBannerBounty = 5,
        QueenBounty = 6,
        ExoticBounty = 7,
        Armor = 8,
        Weapon = 9,
        Engram = 23,
        Consumable = 24,
        ExchangeMaterial = 25,
        PvpTicket = 26,
        MissionReward = 27,
        BountyReward = 28,
        Currency = 29
    }
}
