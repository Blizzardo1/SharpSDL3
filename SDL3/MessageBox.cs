using SDL3.Enums;
using SDL3.Structs;
using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

using static SDL3.Sdl;

namespace SDL3;

public static partial class MessageBox {
    public static unsafe SdlBool ShowMessageBox(ref MessageBoxData messageboxdata, out int buttonid) {
        // Validate input parameters
        if (messageboxdata.NumButtons < 0 || messageboxdata.Buttons == null) {
            throw new ArgumentException("Invalid MessageBoxData: Buttons must be defined and NumButtons must be non-negative.");
        }

        Logger.LogInfo(LogCategory.System, $"Showing message box with title: {Marshal.PtrToStringAnsi((nint)messageboxdata.Title)}");
        // Call the native method
        var result = SDL_ShowMessageBox(ref messageboxdata, out buttonid);

        // Check the result and handle errors
        if (!result) {
            throw new InvalidOperationException("Failed to display the message box.");
        }

        return result;
    }

    public static MessageBoxResult ShowMessageBox(string message) {
        return ShowMessageBox(nint.Zero, message, "Message");
    }

    public static MessageBoxResult ShowMessageBox(string message, string title) {
        return ShowMessageBox(nint.Zero, message, title);
    }

    public static unsafe MessageBoxResult ShowMessageBox(nint windowOwner, string message, string title) {
        return ShowMessageBox(windowOwner, message, title, MessageBoxFlags.Information);
    }

    public static MessageBoxResult ShowMessageBox(nint windowOwner, string message, string title, MessageBoxFlags flags) {
        return ShowMessageBox(windowOwner, message, title, flags, MessageBoxButtons.Ok);
    }

    public static MessageBoxResult ShowMessageBox(nint windowOwner, string message, string title, MessageBoxFlags flags, MessageBoxButtons buttons ) {
        return ShowMessageBox(windowOwner, message, title, flags, buttons, MessageBoxDefaultButton.ReturnKeyDefault);
    }

    public static unsafe MessageBoxResult ShowMessageBox(nint windowOwner, string message, string title, MessageBoxFlags flags, MessageBoxButtons buttons, MessageBoxDefaultButton accelerator) {
        MessageBoxColorScheme scheme = default;
        return ShowMessageBox(windowOwner, message, title, flags, buttons, accelerator, ref scheme);
    }

    public static unsafe MessageBoxResult ShowMessageBox(nint windowOwner, string message, string title, MessageBoxFlags flags, MessageBoxButtons buttons, MessageBoxDefaultButton accelerator, ref MessageBoxColorScheme scheme) {
        if (string.IsNullOrEmpty(title) || string.IsNullOrEmpty(message)) {
            throw new ArgumentException("Title and message cannot be null or empty.");
        }

        object[] yes = ["Yes", MessageBoxResult.Yes, MessageBoxDefaultButton.ReturnKeyDefault];
        object[] no = ["No", MessageBoxResult.No, MessageBoxDefaultButton.EscapeKeyDefault];
        object[] cancel = ["Cancel", MessageBoxResult.Cancel, MessageBoxDefaultButton.EscapeKeyDefault];
        object[] ok = ["Ok", MessageBoxResult.OK, MessageBoxDefaultButton.ReturnKeyDefault];
        object[] retry = ["Retry", MessageBoxResult.Retry, MessageBoxDefaultButton.ReturnKeyDefault];
        object[] ignore = ["Ignore", MessageBoxResult.Ignore, MessageBoxDefaultButton.EscapeKeyDefault];
        object[] abort = ["Abort", MessageBoxResult.Abort, MessageBoxDefaultButton.EscapeKeyDefault];
        object[] tryAgain = ["Try Again", MessageBoxResult.TryAgain, MessageBoxDefaultButton.ReturnKeyDefault];
        object[] continueButton = ["Continue", MessageBoxResult.Continue, MessageBoxDefaultButton.ReturnKeyDefault];
        object[] ignoreAll = ["Ignore All", MessageBoxResult.Ignore, MessageBoxDefaultButton.EscapeKeyDefault];
        object[] noToAll = ["No To All", MessageBoxResult.OK, MessageBoxDefaultButton.ReturnKeyDefault];
        object[] yesToAll = ["Yes To All", MessageBoxResult.OK];
        object[] help = ["Help", MessageBoxResult.OK];
        object[] close = ["Close", MessageBoxResult.OK, MessageBoxDefaultButton.ReturnKeyDefault];
        object[] apply = ["Apply", MessageBoxResult.OK, MessageBoxDefaultButton.ReturnKeyDefault];
        object[] save = ["Save", MessageBoxResult.OK, MessageBoxDefaultButton.ReturnKeyDefault];
        object[] reset = ["Reset", MessageBoxResult.OK, MessageBoxDefaultButton.ReturnKeyDefault];

        // With the following corrected code:
        object[][] buttonData = buttons switch {
            MessageBoxButtons.YesNo => [ no, yes],
            MessageBoxButtons.YesNoCancel => [cancel, no, yes],
            MessageBoxButtons.Ok => [ok],
            MessageBoxButtons.OkCancel => [cancel, ok],
            MessageBoxButtons.AbortRetryIgnore => [ignore, retry, abort],
            MessageBoxButtons.TryAgain => [tryAgain],
            MessageBoxButtons.TryAgainCancel => [cancel, tryAgain],
            MessageBoxButtons.ContinueCancel => [cancel, continueButton],
            MessageBoxButtons.RetryCancel => [cancel, retry],
            MessageBoxButtons.YesNoCancelRetry => [retry, cancel, no, yes],
            MessageBoxButtons.YesNoCancelIgnore => [ignore, cancel, no, yes],
            MessageBoxButtons.HelpClose => [close, help],
            MessageBoxButtons.ApplyCancel => [cancel, apply],
            MessageBoxButtons.SaveCancel => [cancel, save],
            MessageBoxButtons.ResetCancel => [cancel, reset],
            MessageBoxButtons.RetryIgnoreAll => [ignoreAll, retry],
            MessageBoxButtons.NoToAllCancel => [cancel, noToAll],
            MessageBoxButtons.YesToAllCancel => [cancel, yesToAll],
            _ => throw new ArgumentOutOfRangeException(nameof(buttons)),
        };

        var buttonDataArray = new MessageBoxButtonData[buttonData.Length];

        for (int i = 0; i < buttonData.Length; i++) {
            buttonDataArray[i] = new MessageBoxButtonData {
                Flags = accelerator == (MessageBoxDefaultButton)buttonData[i][2] ? accelerator : MessageBoxDefaultButton.EscapeKeyDefault,
                ButtonID = (int)buttonData[i][1],
                Text = (byte*)Marshal.StringToHGlobalAnsi((string)buttonData[i][0])
            };
        }

        var messageboxdata = new MessageBoxData {
            Flags = flags,
            Window = windowOwner,
            Title = (byte*)Marshal.StringToHGlobalAnsi(title),
            Message = (byte*)Marshal.StringToHGlobalAnsi(message),
            NumButtons = buttonData.Length,
            Buttons = (MessageBoxButtonData*)Marshal.UnsafeAddrOfPinnedArrayElement(buttonDataArray, 0), // Use marshaling to pass the array
            ColorScheme = (MessageBoxColorScheme*)Marshal.UnsafeAddrOfPinnedArrayElement([scheme], 0) // Use marshaling to pass the array
        };

        try {
            var result = ShowMessageBox(ref messageboxdata, out int buttonid);

            if (!result) {
                throw new InvalidOperationException("Failed to display the message box.");
            }

            return (MessageBoxResult)buttonid;
        } finally {
            // Ensure all unmanaged resources are freed
            foreach (var button in buttonDataArray) {
                Marshal.FreeHGlobal((nint)button.Text);
            }

            Marshal.FreeHGlobal((nint)messageboxdata.Title);
            Marshal.FreeHGlobal((nint)messageboxdata.Message);
        }
    }

    public static SdlBool ShowSimpleMessageBox(MessageBoxFlags flags, string message, string title, nint window) {
        if (string.IsNullOrEmpty(title) || string.IsNullOrEmpty(message)) {
            throw new ArgumentException("Title and message cannot be null or empty.");
        }
        
        var result = SDL_ShowSimpleMessageBox(flags, title, message, window);

        if (!result) {
            throw new InvalidOperationException("Failed to display the simple message box.");
        }
        return result;
    }

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_ShowMessageBox(ref MessageBoxData messageboxdata, out int buttonid);

    [LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_ShowSimpleMessageBox(MessageBoxFlags flags, string title, string message,
        nint window);
}