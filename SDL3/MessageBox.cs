using SharpSDL3.Enums;
using SharpSDL3.Structs;
using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SharpSDL3;

public static partial class Sdl {

    public static unsafe bool ShowMessageBox(ref MessageBoxData messageboxdata, out int buttonid) {
        // Validate input parameters
        if (messageboxdata.NumButtons < 0 || messageboxdata.Buttons == nint.Zero) {
            throw new ArgumentException("Invalid MessageBoxData: Buttons must be defined and NumButtons must be non-negative.");
        }

        LogInfo(LogCategory.System, $"Showing message box with title: {Marshal.PtrToStringAnsi(messageboxdata.Title)}");
        // Call the native method
        SdlBool result = SDL_ShowMessageBox(ref messageboxdata, out buttonid);

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

    /// <summary>Create a modal message box.</summary>

    /// <param name="messageboxdata">the SDL_MessageBoxData structure with title, text and other options.</param>
    /// <param name="buttonid">the pointer to which user id of hit button should be copied.</param>
    /// <remarks>
    /// If your needs aren't complex, it might be easier to use
    /// SDL_ShowSimpleMessageBox.
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="ShowSimpleMessageBox"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()"/> for more information.</returns>

    public static unsafe MessageBoxResult ShowMessageBox(nint windowOwner, string message, string title) {
        return ShowMessageBox(windowOwner, message, title, MessageBoxFlags.Information);
    }

    public static MessageBoxResult ShowMessageBox(nint windowOwner, string message, string title, MessageBoxFlags flags) {
        return ShowMessageBox(windowOwner, message, title, flags, MessageBoxButtons.Ok);
    }

    public static MessageBoxResult ShowMessageBox(nint windowOwner, string message, string title, MessageBoxFlags flags, MessageBoxButtons buttons) {
        return ShowMessageBox(windowOwner, message, title, flags, buttons, MessageBoxDefaultButton.ReturnKeyDefault);
    }

    /// <summary>Create a modal message box.</summary>

    /// <param name="messageboxdata">the SDL_MessageBoxData structure with title, text and other options.</param>
    /// <param name="buttonid">the pointer to which user id of hit button should be copied.</param>
    /// <remarks>
    /// If your needs aren't complex, it might be easier to use
    /// SDL_ShowSimpleMessageBox.
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="ShowSimpleMessageBox"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()"/> for more information.</returns>

    public static unsafe MessageBoxResult ShowMessageBox(nint windowOwner, string message, string title, MessageBoxFlags flags, MessageBoxButtons buttons, MessageBoxDefaultButton accelerator) {
        MessageBoxColorScheme scheme = default;
        return ShowMessageBox(windowOwner, message, title, flags, buttons, accelerator, ref scheme);
    }

    /// <summary>Create a modal message box.</summary>

    /// <param name="messageboxdata">the SDL_MessageBoxData structure with title, text and other options.</param>
    /// <param name="buttonid">the pointer to which user id of hit button should be copied.</param>
    /// <remarks>
    /// If your needs aren't complex, it might be easier to use
    /// SDL_ShowSimpleMessageBox.
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="ShowSimpleMessageBox"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()"/> for more information.</returns>

    public static unsafe MessageBoxResult ShowMessageBox(nint windowOwner, string message, string title, MessageBoxFlags flags, MessageBoxButtons buttons, MessageBoxDefaultButton accelerator, ref MessageBoxColorScheme scheme) {
        if (string.IsNullOrEmpty(title) || string.IsNullOrEmpty(message)) {
            throw new ArgumentException("Title and message cannot be null or empty.");
        }

        object[] yes = ["Yes", MessageBoxResult.Yes, MessageBoxDefaultButton.ReturnKeyDefault];
        object[] no = ["No", MessageBoxResult.No, MessageBoxDefaultButton.EscapeKeyDefault];
        object[] cancel = ["Cancel", MessageBoxResult.Cancel, MessageBoxDefaultButton.EscapeKeyDefault];
        object[] ok = ["Ok", MessageBoxResult.Ok, MessageBoxDefaultButton.ReturnKeyDefault];
        object[] retry = ["Retry", MessageBoxResult.Retry, MessageBoxDefaultButton.ReturnKeyDefault];
        object[] ignore = ["Ignore", MessageBoxResult.Ignore, MessageBoxDefaultButton.EscapeKeyDefault];
        object[] abort = ["Abort", MessageBoxResult.Abort, MessageBoxDefaultButton.EscapeKeyDefault];
        object[] tryAgain = ["Try Again", MessageBoxResult.TryAgain, MessageBoxDefaultButton.ReturnKeyDefault];
        object[] continueButton = ["Continue", MessageBoxResult.Continue, MessageBoxDefaultButton.ReturnKeyDefault];
        object[] ignoreAll = ["Ignore All", MessageBoxResult.Ignore, MessageBoxDefaultButton.EscapeKeyDefault];
        object[] noToAll = ["No To All", MessageBoxResult.Ok, MessageBoxDefaultButton.ReturnKeyDefault];
        object[] yesToAll = ["Yes To All", MessageBoxResult.Ok];
        object[] help = ["Help", MessageBoxResult.Ok];
        object[] close = ["Close", MessageBoxResult.Ok, MessageBoxDefaultButton.ReturnKeyDefault];
        object[] apply = ["Apply", MessageBoxResult.Ok, MessageBoxDefaultButton.ReturnKeyDefault];
        object[] save = ["Save", MessageBoxResult.Ok, MessageBoxDefaultButton.ReturnKeyDefault];
        object[] reset = ["Reset", MessageBoxResult.Ok, MessageBoxDefaultButton.ReturnKeyDefault];

        // With the following corrected code:
        object[][] buttonData = buttons switch {
            MessageBoxButtons.YesNo => [no, yes],
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
                Text = Marshal.StringToHGlobalAnsi((string)buttonData[i][0])
            };
        }

        var messageboxdata = new MessageBoxData {
            Flags = flags,
            Window = windowOwner,
            Title = Marshal.StringToHGlobalAnsi(title),
            Message = Marshal.StringToHGlobalAnsi(message),
            NumButtons = buttonData.Length,
            Buttons = Marshal.UnsafeAddrOfPinnedArrayElement(buttonDataArray, 0), // Use marshaling to pass the array
            ColorScheme = Marshal.UnsafeAddrOfPinnedArrayElement([scheme], 0) // Use marshaling to pass the array
        };

        try {
            bool result = ShowMessageBox(ref messageboxdata, out int buttonid);

            if (!result) {
                throw new InvalidOperationException("Failed to display the message box.");
            }

            return (MessageBoxResult)buttonid;
        } finally {
            // Ensure all unmanaged resources are freed
            foreach (MessageBoxButtonData button in buttonDataArray) {
                Marshal.FreeHGlobal(button.Text);
            }

            Marshal.FreeHGlobal(messageboxdata.Title);
            Marshal.FreeHGlobal(messageboxdata.Message);
        }
    }

    /// <summary>Display a simple modal message box.</summary>

    /// <param name="flags">an SDL_MessageBoxFlags value.</param>
    /// <param name="title">UTF-8 title text.</param>
    /// <param name="message">UTF-8 message text.</param>
    /// <param name="window">the parent window, or <see langword="null" /> for no parent.</param>
    /// <remarks>
    /// If your needs aren't complex, this function is preferred over
    /// SDL_ShowMessageBox.
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="ShowMessageBox"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()"/> for more information.</returns>

    public static bool ShowSimpleMessageBox(MessageBoxFlags flags, string message, string title, nint window) {
        if (string.IsNullOrEmpty(title) || string.IsNullOrEmpty(message)) {
            throw new ArgumentException("Title and message cannot be null or empty.");
        }

        SdlBool result = SDL_ShowSimpleMessageBox(flags, title, message, window);

        if (!result) {
            throw new InvalidOperationException("Failed to display the simple message box.");
        }
        return result;
    }

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_ShowMessageBox(ref MessageBoxData messageboxdata, out int buttonid);

    [LibraryImport(NativeLibName, StringMarshalling = Marshalling),
     UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_ShowSimpleMessageBox(MessageBoxFlags flags, string title, string message,
        nint window);
}