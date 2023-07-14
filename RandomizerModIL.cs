using System;
using MonoMod.Cil;
using Terraria;
using Terraria.ModLoader;
using static Mono.Cecil.Cil.OpCodes;

namespace RandomizerMod;

public class RandomizerModIL : ModSystem
{
    public override void OnModLoad()
    {
        IL_Recipe.AddRecipe += TryPatch(IL_Recipe_AddRecipe);
    }

    private ILContext.Manipulator TryPatch(ILContext.Manipulator m) => (il) =>
    {
        try { m(il); }
        catch (Exception e) { throw new ILPatchFailureException(Mod, il, e); }
    };

    private static void IL_Recipe_AddRecipe(ILContext il)
    {
        var c = new ILCursor(il);
        while (c.TryGotoNext(MoveType.After, i => i.MatchLdelemRef())) ;

        var breakLabel = (ILLabel)c.Next.Operand;
        c.GotoNext().GotoNext();

        c.Emit(Ldc_I4_0);
        c.Emit(Brfalse_S, breakLabel);
    }
}
