using SharpSDL3.Enums;
using SharpSDL3.Structs;
using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

namespace SharpSDL3;

public static partial class Sdl {
    /// <summary>Acquire a command buffer.</summary>

    /// <param name="device">a GPU context.</param>
    /// <remarks>
    /// This command buffer is managed by the implementation and should not be
    /// freed by the user. The command buffer may only be used on the thread it was
    /// acquired on. The command buffer should be submitted on the thread it was
    /// acquired on.
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="SubmitGPUCommandBuffer"/>
    /// <seealso cref="SubmitGPUCommandBufferAndAcquireFence"/>
    /// </remarks>
    /// <returns>(SDL_GPUCommandBuffer *) Returns a command buffer,or <see langword="null" /> on failure; call <see cref="GetError()" /> for more information.</returns>

    public static nint AcquireGPUCommandBuffer(nint device) {
        if (device == nint.Zero) {
            throw new ArgumentNullException(nameof(device), "Device cannot be null.");
        }
        return SDL_AcquireGPUCommandBuffer(device);
    }

    /// <summary>Acquire a texture to use in presentation.</summary>

    /// <param name="command_buffer">a command buffer.</param>
    /// <param name="window">a window that has been claimed.</param>
    /// <param name="swapchain_texture">a pointer filled in with a swapchain texture handle.</param>
    /// <param name="swapchain_texture_width">a pointer filled in with the swapchain texture width, discarded.</param>
    /// <param name="swapchain_texture_height">a pointer filled in with the swapchain texture height, discarded.</param>
    /// <remarks>
    /// When a swapchain texture is acquired on a command buffer, it will
    /// automatically be submitted for presentation when the command buffer is
    /// submitted. The swapchain texture should only be referenced by the command
    /// buffer used to acquire it.
    /// <para><strong>Thread Safety:</strong> This function should only be called from the thread that created thewindow.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="ClaimWindowForGPUDevice"/>
    /// <seealso cref="SubmitGPUCommandBuffer"/>
    /// <seealso cref="SubmitGPUCommandBufferAndAcquireFence"/>
    /// <seealso cref="CancelGPUCommandBuffer"/>
    /// <seealso cref="GetWindowSizeInPixels"/>
    /// <seealso cref="WaitForGPUSwapchain"/>
    /// <seealso cref="WaitAndAcquireGPUSwapchainTexture"/>
    /// <seealso cref="SetGPUAllowedFramesInFlight"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success, <see langword="false" /> on error; call <see cref="GetError()"/> for more information.</returns>

    public static bool AcquireGPUSwapchainTexture(nint commandBuffer, nint window,
            out nint swapchainTexture, out uint swapchainTextureWidth, out uint swapchainTextureHeight) {
        if (commandBuffer == nint.Zero) {
            throw new ArgumentNullException(nameof(commandBuffer), "Command buffer cannot be null.");
        }
        if (window == nint.Zero) {
            throw new ArgumentNullException(nameof(window), "Window cannot be null.");
        }
        return SDL_AcquireGPUSwapchainTexture(commandBuffer, window, out swapchainTexture, out swapchainTextureWidth, out swapchainTextureHeight);
    }

    /// <summary>Begins a compute pass on a command buffer.</summary>

    /// <param name="command_buffer">a command buffer.</param>
    /// <param name="storage_texture_bindings">an array of writeable storage texture binding structs.</param>
    /// <param name="num_storage_texture_bindings">the number of storage textures to bind from the array.</param>
    /// <param name="storage_buffer_bindings">an array of writeable storage buffer binding structs.</param>
    /// <param name="num_storage_buffer_bindings">the number of storage buffers to bind from the array.</param>
    /// <remarks>
    /// A compute pass is defined by a set of texture subresources and buffers that
    /// may be written to by compute pipelines. These textures and buffers must
    /// have been created with the COMPUTE_STORAGE_WRITE bit or the
    /// COMPUTE_STORAGE_SIMULTANEOUS_READ_WRITE bit. If you do not create a texture
    /// with COMPUTE_STORAGE_SIMULTANEOUS_READ_WRITE, you must not read from the
    /// texture in the compute pass. All operations related to compute pipelines
    /// must take place inside of a compute pass. You must not begin another
    /// compute pass, or a render pass or copy pass before ending the compute pass.
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="EndGPUComputePass"/>
    /// </remarks>
    /// <returns>(SDL_GPUComputePass *) Returns a compute pass handle.</returns>

    public static nint BeginGPUComputePass(nint commandBuffer,
            Span<GpuStorageTextureReadWriteBinding> storageTextureBindings, uint numStorageTextureBindings,
            Span<GpuStorageBufferReadWriteBinding> storageBufferBindings, uint numStorageBufferBindings) {
        if (commandBuffer == nint.Zero) {
            throw new ArgumentNullException(nameof(commandBuffer), "Command buffer cannot be null.");
        }
        return SDL_BeginGPUComputePass(commandBuffer, storageTextureBindings, numStorageTextureBindings,
            storageBufferBindings, numStorageBufferBindings);
    }

    /// <summary>Begins a copy pass on a command buffer.</summary>

    /// <param name="command_buffer">a command buffer.</param>
    /// <remarks>
    /// All operations related to copying to or from buffers or textures take place
    /// inside a copy pass. You must not begin another copy pass, or a render pass
    /// or compute pass before ending the copy pass.
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>(SDL_GPUCopyPass *) Returns a copy pass handle.</returns>

    public static nint BeginGPUCopyPass(nint commandBuffer) {
        if (commandBuffer == nint.Zero) {
            throw new ArgumentNullException(nameof(commandBuffer), "Command buffer cannot be null.");
        }
        return SDL_BeginGPUCopyPass(commandBuffer);
    }

    /// <summary>Begins a render pass on a command buffer.</summary>

    /// <param name="command_buffer">a command buffer.</param>
    /// <param name="color_target_infos">an array of texture subresources with corresponding clear values and load/store ops.</param>
    /// <param name="num_color_targets">the number of color targets in the color_target_infos array.</param>
    /// <param name="depth_stencil_target_info">a texture subresource with corresponding clear value and load/store ops, discarded.</param>
    /// <remarks>
    /// A render pass consists of a set of texture subresources (or depth slices in
    /// the 3D texture case) which will be rendered to during the render pass,
    /// along with corresponding clear values and load/store operations. All
    /// operations related to graphics pipelines must take place inside of a render
    /// pass. A default viewport and scissor state are automatically set when this
    /// is called. You cannot begin another render pass, or begin a compute pass or
    /// copy pass until you have ended the render pass.
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="EndGPURenderPass"/>
    /// </remarks>
    /// <returns>(SDL_GPURenderPass *) Returns a render pass handle.</returns>

    public static nint BeginGPURenderPass(nint commandBuffer, Span<GpuColorTargetInfo> colorTargetInfos,
            uint numColorTargets, in GpuDepthStencilTargetInfo depthStencilTargetInfo) {
        if (commandBuffer == nint.Zero) {
            throw new ArgumentNullException(nameof(commandBuffer), "Command buffer cannot be null.");
        }
        return SDL_BeginGPURenderPass(commandBuffer, colorTargetInfos, numColorTargets, depthStencilTargetInfo);
    }

    /// <summary>Binds a compute pipeline on a command buffer for use in compute dispatch.</summary>

    /// <param name="compute_pass">a compute pass handle.</param>
    /// <param name="compute_pipeline">a compute pipeline to bind.</param>
    /// <remarks>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// </remarks>

    public static void BindGPUComputePipeline(nint computePass, nint computePipeline) {
        if (computePass == nint.Zero) {
            throw new ArgumentNullException(nameof(computePass), "Compute pass cannot be null.");
        }
        SDL_BindGPUComputePipeline(computePass, computePipeline);
    }

    /// <summary>Binds texture-sampler pairs for use on the compute shader.</summary>

    /// <param name="compute_pass">a compute pass handle.</param>
    /// <param name="first_slot">the compute sampler slot to begin binding from.</param>
    /// <param name="texture_sampler_bindings">an array of texture-sampler binding structs.</param>
    /// <param name="num_bindings">the number of texture-sampler bindings to bind from the array.</param>
    /// <remarks>
    /// The textures must have been created with
    /// SDL_GPU_TEXTUREUSAGE_SAMPLER.
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="CreateGPUShader"/>
    /// </remarks>

    public static void BindGPUComputeSamplers(nint computePass, uint firstSlot,
            Span<GpuTextureSamplerBinding> textureSamplerBindings, uint numBindings) {
        if (computePass == nint.Zero) {
            throw new ArgumentNullException(nameof(computePass), "Compute pass cannot be null.");
        }
        SDL_BindGPUComputeSamplers(computePass, firstSlot, textureSamplerBindings, numBindings);
    }

    /// <summary>Binds storage buffers as readonly for use on the compute pipeline.</summary>

    /// <param name="compute_pass">a compute pass handle.</param>
    /// <param name="first_slot">the compute storage buffer slot to begin binding from.</param>
    /// <param name="storage_buffers">an array of storage buffer binding structs.</param>
    /// <param name="num_bindings">the number of storage buffers to bind from the array.</param>
    /// <remarks>
    /// These buffers must have been created with
    /// SDL_GPU_BUFFERUSAGE_COMPUTE_STORAGE_READ.
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="CreateGPUShader"/>
    /// </remarks>

    public static void BindGPUComputeStorageBuffers(nint computePass, uint firstSlot,
            Span<nint> storageBuffers, uint numBindings) {
        if (computePass == nint.Zero) {
            throw new ArgumentNullException(nameof(computePass), "Compute pass cannot be null.");
        }
        SDL_BindGPUComputeStorageBuffers(computePass, firstSlot, storageBuffers, numBindings);
    }

    /// <summary>Binds storage textures as readonly for use on the compute pipeline.</summary>

    /// <param name="compute_pass">a compute pass handle.</param>
    /// <param name="first_slot">the compute storage texture slot to begin binding from.</param>
    /// <param name="storage_textures">an array of storage textures.</param>
    /// <param name="num_bindings">the number of storage textures to bind from the array.</param>
    /// <remarks>
    /// These textures must have been created with
    /// SDL_GPU_TEXTUREUSAGE_COMPUTE_STORAGE_READ.
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="CreateGPUShader"/>
    /// </remarks>

    public static void BindGPUComputeStorageTextures(nint computePass, uint firstSlot,
            Span<nint> storageTextures, uint numBindings) {
        if (computePass == nint.Zero) {
            throw new ArgumentNullException(nameof(computePass), "Compute pass cannot be null.");
        }
        SDL_BindGPUComputeStorageTextures(computePass, firstSlot, storageTextures, numBindings);
    }

    /// <summary>Binds texture-sampler pairs for use on the fragment shader.</summary>

    /// <param name="render_pass">a render pass handle.</param>
    /// <param name="first_slot">the fragment sampler slot to begin binding from.</param>
    /// <param name="texture_sampler_bindings">an array of texture-sampler binding structs.</param>
    /// <param name="num_bindings">the number of texture-sampler pairs to bind from the array.</param>
    /// <remarks>
    /// The textures must have been created with
    /// SDL_GPU_TEXTUREUSAGE_SAMPLER.
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="CreateGPUShader"/>
    /// </remarks>

    public static void BindGPUFragmentSamplers(nint renderPass, uint firstSlot,
            Span<GpuTextureSamplerBinding> textureSamplerBindings, uint numBindings) {
        if (renderPass == nint.Zero) {
            throw new ArgumentNullException(nameof(renderPass), "Render pass cannot be null.");
        }
        SDL_BindGPUFragmentSamplers(renderPass, firstSlot, textureSamplerBindings, numBindings);
    }

    /// <summary>Binds storage buffers for use on the fragment shader.</summary>

    /// <param name="render_pass">a render pass handle.</param>
    /// <param name="first_slot">the fragment storage buffer slot to begin binding from.</param>
    /// <param name="storage_buffers">an array of storage buffers.</param>
    /// <param name="num_bindings">the number of storage buffers to bind from the array.</param>
    /// <remarks>
    /// These buffers must have been created with
    /// SDL_GPU_BUFFERUSAGE_GRAPHICS_STORAGE_READ.
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="CreateGPUShader"/>
    /// </remarks>

    public static void BindGPUFragmentStorageBuffers(nint renderPass, uint firstSlot,
            Span<nint> storageBuffers, uint numBindings) {
        if (renderPass == nint.Zero) {
            throw new ArgumentNullException(nameof(renderPass), "Render pass cannot be null.");
        }
        SDL_BindGPUFragmentStorageBuffers(renderPass, firstSlot, storageBuffers, numBindings);
    }

    /// <summary>Binds storage textures for use on the fragment shader.</summary>

    /// <param name="render_pass">a render pass handle.</param>
    /// <param name="first_slot">the fragment storage texture slot to begin binding from.</param>
    /// <param name="storage_textures">an array of storage textures.</param>
    /// <param name="num_bindings">the number of storage textures to bind from the array.</param>
    /// <remarks>
    /// These textures must have been created with
    /// SDL_GPU_TEXTUREUSAGE_GRAPHICS_STORAGE_READ.
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="CreateGPUShader"/>
    /// </remarks>

    public static void BindGPUFragmentStorageTextures(nint renderPass, uint firstSlot,
            Span<nint> storageTextures, uint numBindings) {
        if (renderPass == nint.Zero) {
            throw new ArgumentNullException(nameof(renderPass), "Render pass cannot be null.");
        }
        SDL_BindGPUFragmentStorageTextures(renderPass, firstSlot, storageTextures, numBindings);
    }

    /// <summary>Binds a graphics pipeline on a render pass to be used in rendering.</summary>

    /// <param name="render_pass">a render pass handle.</param>
    /// <param name="graphics_pipeline">the graphics pipeline to bind.</param>
    /// <remarks>
    /// A graphics pipeline must be bound before making any draw calls.
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// </remarks>

    public static void BindGPUGraphicsPipeline(nint renderPass, nint graphicsPipeline) {
        if (renderPass == nint.Zero) {
            throw new ArgumentNullException(nameof(renderPass), "Render pass cannot be null.");
        }
        SDL_BindGPUGraphicsPipeline(renderPass, graphicsPipeline);
    }

    /// <summary>Binds an index buffer on a command buffer for use with subsequent draw calls.</summary>

    /// <param name="render_pass">a render pass handle.</param>
    /// <param name="binding">a pointer to a struct containing an index buffer and offset.</param>
    /// <param name="index_element_size">whether the index values in the buffer are 16- or 32-bit.</param>
    /// <remarks>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// </remarks>

    public static void BindGPUIndexBuffer(nint renderPass, in GpuBufferBinding binding,
            GpuIndexElementSize indexElementSize) {
        if (renderPass == nint.Zero) {
            throw new ArgumentNullException(nameof(renderPass), "Render pass cannot be null.");
        }
        SDL_BindGPUIndexBuffer(renderPass, binding, indexElementSize);
    }

    /// <summary>Binds vertex buffers on a command buffer for use with subsequent draw calls.</summary>

    /// <param name="render_pass">a render pass handle.</param>
    /// <param name="first_slot">the vertex buffer slot to begin binding from.</param>
    /// <param name="bindings">an array of SDL_GPUBufferBinding structs containing vertex buffers and offset values.</param>
    /// <param name="num_bindings">the number of bindings in the bindings array.</param>
    /// <remarks>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// </remarks>

    public static void BindGPUVertexBuffers(nint renderPass, uint firstSlot,
            Span<GpuBufferBinding> bindings, uint numBindings) {
        if (renderPass == nint.Zero) {
            throw new ArgumentNullException(nameof(renderPass), "Render pass cannot be null.");
        }
        SDL_BindGPUVertexBuffers(renderPass, firstSlot, bindings, numBindings);
    }

    /// <summary>Binds texture-sampler pairs for use on the vertex shader.</summary>

    /// <param name="render_pass">a render pass handle.</param>
    /// <param name="first_slot">the vertex sampler slot to begin binding from.</param>
    /// <param name="texture_sampler_bindings">an array of texture-sampler binding structs.</param>
    /// <param name="num_bindings">the number of texture-sampler pairs to bind from the array.</param>
    /// <remarks>
    /// The textures must have been created with
    /// SDL_GPU_TEXTUREUSAGE_SAMPLER.
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="CreateGPUShader"/>
    /// </remarks>

    public static void BindGPUVertexSamplers(nint renderPass, uint firstSlot,
            Span<GpuTextureSamplerBinding> textureSamplerBindings, uint numBindings) {
        if (renderPass == nint.Zero) {
            throw new ArgumentNullException(nameof(renderPass), "Render pass cannot be null.");
        }
        SDL_BindGPUVertexSamplers(renderPass, firstSlot, textureSamplerBindings, numBindings);
    }

    /// <summary>Binds storage buffers for use on the vertex shader.</summary>

    /// <param name="render_pass">a render pass handle.</param>
    /// <param name="first_slot">the vertex storage buffer slot to begin binding from.</param>
    /// <param name="storage_buffers">an array of buffers.</param>
    /// <param name="num_bindings">the number of buffers to bind from the array.</param>
    /// <remarks>
    /// These buffers must have been created with
    /// SDL_GPU_BUFFERUSAGE_GRAPHICS_STORAGE_READ.
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="CreateGPUShader"/>
    /// </remarks>

    public static void BindGPUVertexStorageBuffers(nint renderPass, uint firstSlot,
            Span<nint> storageBuffers, uint numBindings) {
        if (renderPass == nint.Zero) {
            throw new ArgumentNullException(nameof(renderPass), "Render pass cannot be null.");
        }
        SDL_BindGPUVertexStorageBuffers(renderPass, firstSlot, storageBuffers, numBindings);
    }

    /// <summary>Binds storage textures for use on the vertex shader.</summary>

    /// <param name="render_pass">a render pass handle.</param>
    /// <param name="first_slot">the vertex storage texture slot to begin binding from.</param>
    /// <param name="storage_textures">an array of storage textures.</param>
    /// <param name="num_bindings">the number of storage texture to bind from the array.</param>
    /// <remarks>
    /// These textures must have been created with
    /// SDL_GPU_TEXTUREUSAGE_GRAPHICS_STORAGE_READ.
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="CreateGPUShader"/>
    /// </remarks>

    public static void BindGPUVertexStorageTextures(nint renderPass, uint firstSlot,
            Span<nint> storageTextures, uint numBindings) {
        if (renderPass == nint.Zero) {
            throw new ArgumentNullException(nameof(renderPass), "Render pass cannot be null.");
        }
        SDL_BindGPUVertexStorageTextures(renderPass, firstSlot, storageTextures, numBindings);
    }

    /// <summary>Blits from a source texture region to a destination texture region.</summary>

    /// <param name="command_buffer">a command buffer.</param>
    /// <param name="info">the blit info struct containing the blit parameters.</param>
    /// <remarks>
    /// This function must not be called inside of any pass.
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// </remarks>

    public static void BlitGPUTexture(nint commandBuffer, in GpuBlitInfo info) {
        if (commandBuffer == nint.Zero) {
            throw new ArgumentNullException(nameof(commandBuffer), "Command buffer cannot be null.");
        }
        SDL_BlitGPUTexture(commandBuffer, info);
    }

    /// <summary>Calculate the size in bytes of a texture format with dimensions.</summary>

    /// <param name="format">a texture format.</param>
    /// <param name="width">width in pixels.</param>
    /// <param name="height">height in pixels.</param>
    /// <param name="depth_or_layer_count">depth for 3D textures or layer count otherwise.</param>
    /// <remarks>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>Returns the size of a texture with this format anddimensions.</returns>

    public static uint CalculateGPUTextureFormatSize(GpuTextureFormat format, uint width, uint height, uint depthOrLayerCount) {
        if (!Enum.IsDefined(format)) {
            throw new ArgumentOutOfRangeException(nameof(format), "Invalid GpuTextureFormat value.");
        }
        return SDL_CalculateGPUTextureFormatSize(format, width, height, depthOrLayerCount);
    }

    /// <summary>Cancels a command buffer.</summary>

    /// <param name="command_buffer">a command buffer.</param>
    /// <remarks>
    /// None of the enqueued commands are executed.
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="WaitAndAcquireGPUSwapchainTexture"/>
    /// <seealso cref="AcquireGPUCommandBuffer"/>
    /// <seealso cref="AcquireGPUSwapchainTexture"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success, <see langword="false" /> on error; call <see cref="GetError()"/> for more information.</returns>

    public static bool CancelGPUCommandBuffer(nint commandBuffer) {
        if (commandBuffer == nint.Zero) {
            throw new ArgumentNullException(nameof(commandBuffer), "Command buffer cannot be null.");
        }
        return SDL_CancelGPUCommandBuffer(commandBuffer);
    }

    /// <summary>Claims a window, creating a swapchain structure for it.</summary>

    /// <param name="device">a GPU context.</param>
    /// <param name="window">an SDL_Window.</param>
    /// <remarks>
    /// This must be called before
    /// SDL_AcquireGPUSwapchainTexture is called
    /// using the window. You should only call this function from the thread that
    /// created the window.
    /// <para><strong>Thread Safety:</strong> This function should only be called from the thread that created thewindow.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="WaitAndAcquireGPUSwapchainTexture"/>
    /// <seealso cref="ReleaseWindowFromGPUDevice"/>
    /// <seealso cref="WindowSupportsGPUPresentMode"/>
    /// <seealso cref="WindowSupportsGPUSwapchainComposition"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success, or <see langword="false" /> on failure; call <see cref="GetError()"/> for more information.</returns>

    public static bool ClaimWindowForGPUDevice(nint device, nint window) {
        if (device == nint.Zero) {
            throw new ArgumentNullException(nameof(device), "Device cannot be null.");
        }
        if (window == nint.Zero) {
            throw new ArgumentNullException(nameof(window), "Window cannot be null.");
        }
        return SDL_ClaimWindowForGPUDevice(device, window);
    }

    /// <summary>Performs a buffer-to-buffer copy.</summary>

    /// <param name="copy_pass">a copy pass handle.</param>
    /// <param name="source">the buffer and offset to copy from.</param>
    /// <param name="destination">the buffer and offset to copy to.</param>
    /// <param name="size">the length of the buffer to copy.</param>
    /// <param name="cycle">if <see langword="true" />, cycles the destination buffer if it is already bound, otherwise overwrites the data.</param>
    /// <remarks>
    /// This copy occurs on the GPU timeline. You may assume the copy has finished
    /// in subsequent commands.
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// </remarks>

    public static void CopyGPUBufferToBuffer(nint copyPass, in GpuBufferLocation source,
            in GpuBufferLocation destination, uint size, bool cycle) {
        if (copyPass == nint.Zero) {
            throw new ArgumentNullException(nameof(copyPass), "Copy pass cannot be null.");
        }
        SDL_CopyGPUBufferToBuffer(copyPass, source, destination, size, cycle);
    }

    /// <summary>Performs a texture-to-texture copy.</summary>

    /// <param name="copy_pass">a copy pass handle.</param>
    /// <param name="source">a source texture region.</param>
    /// <param name="destination">a destination texture region.</param>
    /// <param name="w">the width of the region to copy.</param>
    /// <param name="h">the height of the region to copy.</param>
    /// <param name="d">the depth of the region to copy.</param>
    /// <param name="cycle">if <see langword="true" />, cycles the destination texture if the destination texture is bound, otherwise overwrites the data.</param>
    /// <remarks>
    /// This copy occurs on the GPU timeline. You may assume the copy has finished
    /// in subsequent commands.
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// </remarks>

    public static void CopyGPUTextureToTexture(nint copyPass, in GpuTextureLocation source,
            in GpuTextureLocation destination, uint w, uint h, uint d, bool cycle) {
        if (copyPass == nint.Zero) {
            throw new ArgumentNullException(nameof(copyPass), "Copy pass cannot be null.");
        }
        SDL_CopyGPUTextureToTexture(copyPass, source, destination, w, h, d, cycle);
    }

    /// <summary>Creates a buffer object to be used in graphics or compute workflows.</summary>

    /// <param name="device">a GPU Context.</param>
    /// <param name="createinfo">a struct describing the state of the buffer to create.</param>
    /// <remarks>
    /// The contents of this buffer are undefined until data is written to the
    /// buffer.
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="UploadToGPUBuffer"/>
    /// <seealso cref="DownloadFromGPUBuffer"/>
    /// <seealso cref="CopyGPUBufferToBuffer"/>
    /// <seealso cref="BindGPUVertexBuffers"/>
    /// <seealso cref="BindGPUIndexBuffer"/>
    /// <seealso cref="BindGPUVertexStorageBuffers"/>
    /// <seealso cref="BindGPUFragmentStorageBuffers"/>
    /// <seealso cref="DrawGPUPrimitivesIndirect"/>
    /// <seealso cref="DrawGPUIndexedPrimitivesIndirect"/>
    /// <seealso cref="BindGPUComputeStorageBuffers"/>
    /// <seealso cref="DispatchGPUComputeIndirect"/>
    /// <seealso cref="ReleaseGPUBuffer"/>
    /// </remarks>
    /// <returns>(SDL_GPUBuffer *) Returns a buffer object on success, or<see langword="null" /> on failure; call <see cref="GetError()" /> for more information.</returns>

    public static nint CreateGPUBuffer(nint device, in GpuBufferCreateInfo createinfo) {
        if (device == nint.Zero) {
            throw new ArgumentNullException(nameof(device), "Device cannot be null.");
        }
        return SDL_CreateGPUBuffer(device, createinfo);
    }

    /// <summary>Creates a pipeline object to be used in a compute workflow.</summary>

    /// <param name="device">a GPU Context.</param>
    /// <param name="createinfo">a struct describing the state of the compute pipeline to create.</param>
    /// <remarks>
    /// Shader resource bindings must be authored to follow a particular order
    /// depending on the shader format.
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="BindGPUComputePipeline"/>
    /// <seealso cref="ReleaseGPUComputePipeline"/>
    /// </remarks>
    /// <returns>(SDL_GPUComputePipeline *) Returns a computepipeline object on success, or <see langword="null" /> on failure; call <see cref="GetError()"/> for more information.</returns>

    public static nint CreateGPUComputePipeline(nint device, in GpuComputePipelineCreateInfo createinfo) {
        if (device == nint.Zero) {
            throw new ArgumentNullException(nameof(device), "Device cannot be null.");
        }
        return SDL_CreateGPUComputePipeline(device, createinfo);
    }

    /// <summary>Creates a GPU context.</summary>

    /// <param name="format_flags">a bitflag indicating which shader formats the app is able to provide.</param>
    /// <param name="debug_mode">enable debug mode properties and validations.</param>
    /// <param name="name">the preferred GPU driver, or <see langword="null" /> to let SDL pick the optimal driver.</param>
    /// <remarks>
    /// The GPU driver name can be one of the following:
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="CreateGPUDeviceWithProperties"/>
    /// <seealso cref="GetGPUShaderFormats"/>
    /// <seealso cref="GetGPUDeviceDriver"/>
    /// <seealso cref="DestroyGPUDevice"/>
    /// <seealso cref="GPUSupportsShaderFormats"/>
    /// </remarks>
    /// <returns>(SDL_GPUDevice *) Returns a GPU context on success or <see langword="null" />on failure; call <see cref="GetError()" /> for more information.</returns>

    public static nint CreateGPUDevice(GpuShaderFormat formatFlags, bool debugMode, string name) {
        if (!Enum.IsDefined(formatFlags)) {
            throw new ArgumentOutOfRangeException(nameof(formatFlags), "Invalid GpuShaderFormat value.");
        }
        if (string.IsNullOrEmpty(name)) {
            throw new ArgumentException("Name cannot be null or empty.", nameof(name));
        }
        return SDL_CreateGPUDevice(formatFlags, debugMode, name);
    }

    /// <summary>Creates a GPU context.</summary>

    /// <param name="props">the properties to use.</param>
    /// <remarks>
    /// These are the supported properties:
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetGPUShaderFormats"/>
    /// <seealso cref="GetGPUDeviceDriver"/>
    /// <seealso cref="DestroyGPUDevice"/>
    /// <seealso cref="GPUSupportsProperties"/>
    /// </remarks>
    /// <returns>(SDL_GPUDevice *) Returns a GPU context on success or <see langword="null" />on failure; call <see cref="GetError()" /> for more information.</returns>

    public static nint CreateGPUDeviceWithProperties(uint props) {
        return SDL_CreateGPUDeviceWithProperties(props);
    }

    /// <summary>Creates a pipeline object to be used in a graphics workflow.</summary>

    /// <param name="device">a GPU Context.</param>
    /// <param name="createinfo">a struct describing the state of the graphics pipeline to create.</param>
    /// <remarks>
    /// There are optional properties that can be provided through props. These
    /// are the supported properties:
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="CreateGPUShader"/>
    /// <seealso cref="BindGPUGraphicsPipeline"/>
    /// <seealso cref="ReleaseGPUGraphicsPipeline"/>
    /// </remarks>
    /// <returns>(SDL_GPUGraphicsPipeline *) Returns a graphicspipeline object on success, or <see langword="null" /> on failure; call <see cref="GetError()"/> for more information.</returns>

    public static nint CreateGPUGraphicsPipeline(nint device, in GpuGraphicsPipelineCreateInfo createinfo) {
        if (device == nint.Zero) {
            throw new ArgumentNullException(nameof(device), "Device cannot be null.");
        }
        return SDL_CreateGPUGraphicsPipeline(device, createinfo);
    }

    /// <summary>Creates a sampler object to be used when binding textures in a graphics workflow.</summary>

    /// <param name="device">a GPU Context.</param>
    /// <param name="createinfo">a struct describing the state of the sampler to create.</param>
    /// <remarks>
    /// There are optional properties that can be provided through props. These
    /// are the supported properties:
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="BindGPUVertexSamplers"/>
    /// <seealso cref="BindGPUFragmentSamplers"/>
    /// <seealso cref="ReleaseGPUSampler"/>
    /// </remarks>
    /// <returns>(SDL_GPUSampler *) Returns a sampler object on success,or <see langword="null" /> on failure; call <see cref="GetError()" /> for more information.</returns>

    public static nint CreateGPUSampler(nint device, in GpuSamplerCreateInfo createinfo) {
        if (device == nint.Zero) {
            throw new ArgumentNullException(nameof(device), "Device cannot be null.");
        }
        return SDL_CreateGPUSampler(device, createinfo);
    }

    /// <summary>Creates a shader to be used when creating a graphics pipeline.</summary>

    /// <param name="device">a GPU Context.</param>
    /// <param name="createinfo">a struct describing the state of the shader to create.</param>
    /// <remarks>
    /// Shader resource bindings must be authored to follow a particular order
    /// depending on the shader format.
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="CreateGPUGraphicsPipeline"/>
    /// <seealso cref="ReleaseGPUShader"/>
    /// </remarks>
    /// <returns>(SDL_GPUShader *) Returns a shader object on success, or<see langword="null" /> on failure; call <see cref="GetError()" /> for more information.</returns>

    public static nint CreateGPUShader(nint device, in GpuShaderCreateInfo createinfo) {
        if (device == nint.Zero) {
            throw new ArgumentNullException(nameof(device), "Device cannot be null.");
        }
        return SDL_CreateGPUShader(device, createinfo);
    }

    /// <summary>Creates a texture object to be used in graphics or compute workflows.</summary>

    /// <param name="device">a GPU Context.</param>
    /// <param name="createinfo">a struct describing the state of the texture to create.</param>
    /// <remarks>
    /// The contents of this texture are undefined until data is written to the
    /// texture.
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="UploadToGPUTexture"/>
    /// <seealso cref="DownloadFromGPUTexture"/>
    /// <seealso cref="BindGPUVertexSamplers"/>
    /// <seealso cref="BindGPUVertexStorageTextures"/>
    /// <seealso cref="BindGPUFragmentSamplers"/>
    /// <seealso cref="BindGPUFragmentStorageTextures"/>
    /// <seealso cref="BindGPUComputeStorageTextures"/>
    /// <seealso cref="BlitGPUTexture"/>
    /// <seealso cref="ReleaseGPUTexture"/>
    /// <seealso cref="GPUTextureSupportsFormat"/>
    /// </remarks>
    /// <returns>(SDL_GPUTexture *) Returns a texture object on success,or <see langword="null" /> on failure; call <see cref="GetError()" /> for more information.</returns>

    public static nint CreateGPUTexture(nint device, in GpuTextureCreateInfo createinfo) {
        if (device == nint.Zero) {
            throw new ArgumentNullException(nameof(device), "Device cannot be null.");
        }
        return SDL_CreateGPUTexture(device, createinfo);
    }

    /// <summary>Creates a transfer buffer to be used when uploading to or downloading from graphics resources.</summary>

    /// <param name="device">a GPU Context.</param>
    /// <param name="createinfo">a struct describing the state of the transfer buffer to create.</param>
    /// <remarks>
    /// Download buffers can be particularly expensive to create, so it is good
    /// practice to reuse them if data will be downloaded regularly.
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="UploadToGPUBuffer"/>
    /// <seealso cref="DownloadFromGPUBuffer"/>
    /// <seealso cref="UploadToGPUTexture"/>
    /// <seealso cref="DownloadFromGPUTexture"/>
    /// <seealso cref="ReleaseGPUTransferBuffer"/>
    /// </remarks>
    /// <returns>(SDL_GPUTransferBuffer *) Returns a transferbuffer on success, or <see langword="null" /> on failure; call <see cref="GetError()" />for more information.</returns>

    public static nint CreateGPUTransferBuffer(nint device, in GpuTransferBufferCreateInfo createinfo) {
        if (device == nint.Zero) {
            throw new ArgumentNullException(nameof(device), "Device cannot be null.");
        }
        return SDL_CreateGPUTransferBuffer(device, createinfo);
    }

    /// <summary>Destroys a GPU context previously returned by SDL_CreateGPUDevice.</summary>

    /// <param name="device">a GPU Context to destroy.</param>
    /// <remarks>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="CreateGPUDevice"/>
    /// </remarks>

    public static void DestroyGPUDevice(nint device) {
        if (device == nint.Zero) {
            throw new ArgumentNullException(nameof(device), "Device cannot be null.");
        }
        SDL_DestroyGPUDevice(device);
    }

    /// <summary>Dispatches compute work.</summary>

    /// <param name="compute_pass">a compute pass handle.</param>
    /// <param name="groupcount_x">number of local workgroups to dispatch in the X dimension.</param>
    /// <param name="groupcount_y">number of local workgroups to dispatch in the Y dimension.</param>
    /// <param name="groupcount_z">number of local workgroups to dispatch in the Z dimension.</param>
    /// <remarks>
    /// You must not call this function before binding a compute pipeline.
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// </remarks>

    public static void DispatchGPUCompute(nint computePass, uint groupcountX, uint groupcountY, uint groupcountZ) {
        if (computePass == nint.Zero) {
            throw new ArgumentNullException(nameof(computePass), "Compute pass cannot be null.");
        }
        SDL_DispatchGPUCompute(computePass, groupcountX, groupcountY, groupcountZ);
    }

    /// <summary>Dispatches compute work with parameters set from a buffer.</summary>

    /// <param name="compute_pass">a compute pass handle.</param>
    /// <param name="buffer">a buffer containing dispatch parameters.</param>
    /// <param name="offset">the offset to start reading from the dispatch buffer.</param>
    /// <remarks>
    /// The buffer layout should match the layout of
    /// SDL_GPUIndirectDispatchCommand. You must
    /// not call this function before binding a compute pipeline.
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// </remarks>

    public static void DispatchGPUComputeIndirect(nint computePass, nint buffer, uint offset) {
        if (computePass == nint.Zero) {
            throw new ArgumentNullException(nameof(computePass), "Compute pass cannot be null.");
        }
        SDL_DispatchGPUComputeIndirect(computePass, buffer, offset);
    }

    /// <summary>Copies data from a buffer to a transfer buffer on the GPU timeline.</summary>

    /// <param name="copy_pass">a copy pass handle.</param>
    /// <param name="source">the source buffer with offset and size.</param>
    /// <param name="destination">the destination transfer buffer with offset.</param>
    /// <remarks>
    /// This data is not guaranteed to be copied until the command buffer fence is
    /// signaled.
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// </remarks>

    public static void DownloadFromGPUBuffer(nint copyPass, in GpuBufferRegion source,
            in GpuTransferBufferLocation destination) {
        if (copyPass == nint.Zero) {
            throw new ArgumentNullException(nameof(copyPass), "Copy pass cannot be null.");
        }
        SDL_DownloadFromGPUBuffer(copyPass, source, destination);
    }

    /// <summary>Copies data from a texture to a transfer buffer on the GPU timeline.</summary>

    /// <param name="copy_pass">a copy pass handle.</param>
    /// <param name="source">the source texture region.</param>
    /// <param name="destination">the destination transfer buffer with image layout information.</param>
    /// <remarks>
    /// This data is not guaranteed to be copied until the command buffer fence is
    /// signaled.
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// </remarks>

    public static void DownloadFromGPUTexture(nint copyPass, in GpuTextureRegion source,
            in GpuTextureTransferInfo destination) {
        if (copyPass == nint.Zero) {
            throw new ArgumentNullException(nameof(copyPass), "Copy pass cannot be null.");
        }
        SDL_DownloadFromGPUTexture(copyPass, source, destination);
    }

    /// <summary>Draws data using bound graphics state with an index buffer and instancing enabled.</summary>

    /// <param name="render_pass">a render pass handle.</param>
    /// <param name="num_indices">the number of indices to draw per instance.</param>
    /// <param name="num_instances">the number of instances to draw.</param>
    /// <param name="first_index">the starting index within the index buffer.</param>
    /// <param name="vertex_offset">value added to vertex index before indexing into the vertex buffer.</param>
    /// <param name="first_instance">the ID of the first instance to draw.</param>
    /// <remarks>
    /// You must not call this function before binding a graphics pipeline.
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// </remarks>

    public static void DrawGPUIndexedPrimitives(nint renderPass, uint numIndices, uint numInstances,
            uint firstIndex, int vertexOffset, uint firstInstance) {
        if (renderPass == nint.Zero) {
            throw new ArgumentNullException(nameof(renderPass), "Render pass cannot be null.");
        }
        SDL_DrawGPUIndexedPrimitives(renderPass, numIndices, numInstances, firstIndex, vertexOffset, firstInstance);
    }

    /// <summary>Draws data using bound graphics state with an index buffer enabled and with draw parameters set from a buffer.</summary>

    /// <param name="render_pass">a render pass handle.</param>
    /// <param name="buffer">a buffer containing draw parameters.</param>
    /// <param name="offset">the offset to start reading from the draw buffer.</param>
    /// <param name="draw_count">the number of draw parameter sets that should be read from the draw buffer.</param>
    /// <remarks>
    /// The buffer must consist of tightly-packed draw parameter sets that each
    /// match the layout of
    /// SDL_GPUIndexedIndirectDrawCommand. You
    /// must not call this function before binding a graphics pipeline.
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// </remarks>

    public static void DrawGPUIndexedPrimitivesIndirect(nint renderPass, nint buffer, uint offset, uint drawCount) {
        if (renderPass == nint.Zero) {
            throw new ArgumentNullException(nameof(renderPass), "Render pass cannot be null.");
        }
        SDL_DrawGPUIndexedPrimitivesIndirect(renderPass, buffer, offset, drawCount);
    }

    /// <summary>Draws data using bound graphics state.</summary>

    /// <param name="render_pass">a render pass handle.</param>
    /// <param name="num_vertices">the number of vertices to draw.</param>
    /// <param name="num_instances">the number of instances that will be drawn.</param>
    /// <param name="first_vertex">the index of the first vertex to draw.</param>
    /// <param name="first_instance">the ID of the first instance to draw.</param>
    /// <remarks>
    /// You must not call this function before binding a graphics pipeline.
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// </remarks>

    public static void DrawGPUPrimitives(nint renderPass, uint numVertices, uint numInstances,
            uint firstVertex, uint firstInstance) {
        if (renderPass == nint.Zero) {
            throw new ArgumentNullException(nameof(renderPass), "Render pass cannot be null.");
        }
        SDL_DrawGPUPrimitives(renderPass, numVertices, numInstances, firstVertex, firstInstance);
    }

    /// <summary>Draws data using bound graphics state and with draw parameters set from a buffer.</summary>

    /// <param name="render_pass">a render pass handle.</param>
    /// <param name="buffer">a buffer containing draw parameters.</param>
    /// <param name="offset">the offset to start reading from the draw buffer.</param>
    /// <param name="draw_count">the number of draw parameter sets that should be read from the draw buffer.</param>
    /// <remarks>
    /// The buffer must consist of tightly-packed draw parameter sets that each
    /// match the layout of
    /// SDL_GPUIndirectDrawCommand. You must not call
    /// this function before binding a graphics pipeline.
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// </remarks>

    public static void DrawGPUPrimitivesIndirect(nint renderPass, nint buffer, uint offset, uint drawCount) {
        if (renderPass == nint.Zero) {
            throw new ArgumentNullException(nameof(renderPass), "Render pass cannot be null.");
        }
        SDL_DrawGPUPrimitivesIndirect(renderPass, buffer, offset, drawCount);
    }

    /// <summary>Ends the current compute pass.</summary>

    /// <param name="compute_pass">a compute pass handle.</param>
    /// <remarks>
    /// All bound compute state on the command buffer is unset. The compute pass
    /// handle is now invalid.
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// </remarks>

    public static void EndGPUComputePass(nint computePass) {
        if (computePass == nint.Zero) {
            throw new ArgumentNullException(nameof(computePass), "Compute pass cannot be null.");
        }
        SDL_EndGPUComputePass(computePass);
    }

    /// <summary>Ends the current copy pass.</summary>

    /// <param name="copy_pass">a copy pass handle.</param>
    /// <remarks>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// </remarks>

    public static void EndGPUCopyPass(nint copyPass) {
        if (copyPass == nint.Zero) {
            throw new ArgumentNullException(nameof(copyPass), "Copy pass cannot be null.");
        }
        SDL_EndGPUCopyPass(copyPass);
    }

    /// <summary>Ends the given render pass.</summary>

    /// <param name="render_pass">a render pass handle.</param>
    /// <remarks>
    /// All bound graphics state on the render pass command buffer is unset. The
    /// render pass handle is now invalid.
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// </remarks>

    public static void EndGPURenderPass(nint renderPass) {
        if (renderPass == nint.Zero) {
            throw new ArgumentNullException(nameof(renderPass), "Render pass cannot be null.");
        }
        SDL_EndGPURenderPass(renderPass);
    }

    /// <summary>Generates mipmaps for the given texture.</summary>

    /// <param name="command_buffer">a command_buffer.</param>
    /// <param name="texture">a texture with more than 1 mip level.</param>
    /// <remarks>
    /// This function must not be called inside of any pass.
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// </remarks>

    public static void GenerateMipmapsForGPUTexture(nint commandBuffer, nint texture) {
        if (commandBuffer == nint.Zero) {
            throw new ArgumentNullException(nameof(commandBuffer), "Command buffer cannot be null.");
        }
        if (texture == nint.Zero) {
            throw new ArgumentNullException(nameof(texture), "Texture cannot be null.");
        }
        SDL_GenerateMipmapsForGPUTexture(commandBuffer, texture);
    }

    /// <summary>Returns the name of the backend used to create this GPU context.</summary>

    /// <param name="device">a GPU context to query.</param>
    /// <remarks>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>(const char *) Returns the name of the device's driver, or <see langword="null" /> on error.</returns>

    public static string GetGPUDeviceDriver(nint device) {
        if (device == nint.Zero) {
            throw new ArgumentNullException(nameof(device), "Device cannot be null.");
        }
        return SDL_GetGPUDeviceDriver(device);
    }

    /// <summary>Get the name of a built in GPU driver.</summary>

    /// <param name="index">the index of a GPU driver.</param>
    /// <remarks>
    /// The GPU drivers are presented in the order in which they are normally
    /// checked during initialization.
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetNumGPUDrivers"/>
    /// </remarks>
    /// <returns>(const char *) Returns the name of the GPU driver with the given index.</returns>

    public static string GetGPUDriver(int index) {
        if (index < 0) {
            throw new ArgumentOutOfRangeException(nameof(index), "Index must be non-negative.");
        }
        return SDL_GetGPUDriver(index);
    }

    /// <summary>Returns the supported shader formats for this GPU context.</summary>

    /// <param name="device">a GPU context to query.</param>
    /// <remarks>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>Returns a bitflag indicatingwhich shader formats the driver is able to consume.</returns>

    public static GpuShaderFormat GetGPUShaderFormats(nint device) {
        if (device == nint.Zero) {
            throw new ArgumentNullException(nameof(device), "Device cannot be null.");
        }
        return SDL_GetGPUShaderFormats(device);
    }

    /// <summary>Obtains the texture format of the swapchain for the given window.</summary>

    /// <param name="device">a GPU context.</param>
    /// <param name="window">an SDL_Window that has been claimed.</param>
    /// <remarks>
    /// Note that this format can change if the swapchain parameters change.
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>Returns the texture formatof the swapchain.</returns>

    public static GpuTextureFormat GetGPUSwapchainTextureFormat(nint device, nint window) {
        if (device == nint.Zero) {
            throw new ArgumentNullException(nameof(device), "Device cannot be null.");
        }
        if (window == nint.Zero) {
            throw new ArgumentNullException(nameof(window), "Window cannot be null.");
        }
        return SDL_GetGPUSwapchainTextureFormat(device, window);
    }

    /// <summary>Get the number of GPU drivers compiled into SDL.</summary>
    /// <remarks>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetGPUDriver"/>
    /// </remarks>
    /// <returns>Returns the number of built in GPU drivers.</returns>

    public static int GetNumGPUDrivers() {
        return SDL_GetNumGPUDrivers();
    }

    /// <summary>Checks for GPU runtime support.</summary>

    /// <param name="props">the properties to use.</param>
    /// <remarks>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="CreateGPUDeviceWithProperties"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> if supported, <see langword="false" /> otherwise.</returns>

    public static bool GPUSupportsProperties(uint props) {
        return SDL_GPUSupportsProperties(props);
    }

    /// <summary>Checks for GPU runtime support.</summary>

    /// <param name="format_flags">a bitflag indicating which shader formats the app is able to provide.</param>
    /// <param name="name">the preferred GPU driver, or <see langword="null" /> to let SDL pick the optimal driver.</param>
    /// <remarks>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="CreateGPUDevice"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> if supported, <see langword="false" /> otherwise.</returns>

    public static bool GPUSupportsShaderFormats(GpuShaderFormat formatFlags, string name) {
        if (!Enum.IsDefined(formatFlags)) {
            throw new ArgumentOutOfRangeException(nameof(formatFlags), "Invalid GpuShaderFormat value.");
        }
        if (string.IsNullOrEmpty(name)) {
            throw new ArgumentException("Name cannot be null or empty.", nameof(name));
        }
        return SDL_GPUSupportsShaderFormats(formatFlags, name);
    }

    /// <summary>Obtains the texel block size for a texture format.</summary>

    /// <param name="format">the texture format you want to know the texel size of.</param>
    /// <remarks>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="UploadToGPUTexture"/>
    /// </remarks>
    /// <returns>Returns the texel block size of the texture format.</returns>

    public static uint GPUTextureFormatTexelBlockSize(GpuTextureFormat format) {
        if (!Enum.IsDefined(format)) {
            throw new ArgumentOutOfRangeException(nameof(format), "Invalid GpuTextureFormat value.");
        }
        return SDL_GPUTextureFormatTexelBlockSize(format);
    }

    /// <summary>Determines whether a texture format is supported for a given type and usage.</summary>

    /// <param name="device">a GPU context.</param>
    /// <param name="format">the texture format to check.</param>
    /// <param name="type">the type of texture (2D, 3D, Cube).</param>
    /// <param name="usage">a bitmask of all usage scenarios to check.</param>
    /// <remarks>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>Returns whether the texture format is supported for this type andusage.</returns>

    public static bool GPUTextureSupportsFormat(nint device, GpuTextureFormat format, GpuTextureType type, GpuTextureUsageFlags usage) {
        if (device == nint.Zero) {
            throw new ArgumentNullException(nameof(device), "Device cannot be null.");
        }
        if (!Enum.IsDefined(format)) {
            throw new ArgumentOutOfRangeException(nameof(format), "Invalid GpuTextureFormat value.");
        }
        if (!Enum.IsDefined(type)) {
            throw new ArgumentOutOfRangeException(nameof(type), "Invalid GpuTextureType value.");
        }
        return SDL_GPUTextureSupportsFormat(device, format, type, usage);
    }

    /// <summary>Determines if a sample count for a texture format is supported.</summary>

    /// <param name="device">a GPU context.</param>
    /// <param name="format">the texture format to check.</param>
    /// <param name="sample_count">the sample count to check.</param>
    /// <remarks>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>Returns whether the sample count is supported for this textureformat.</returns>

    public static bool GPUTextureSupportsSampleCount(nint device, GpuTextureFormat format, GpuSampleCount sampleCount) {
        if (device == nint.Zero) {
            throw new ArgumentNullException(nameof(device), "Device cannot be null.");
        }
        if (!Enum.IsDefined(format)) {
            throw new ArgumentOutOfRangeException(nameof(format), "Invalid GpuTextureFormat value.");
        }
        if (!Enum.IsDefined(sampleCount)) {
            throw new ArgumentOutOfRangeException(nameof(sampleCount), "Invalid GpuSampleCount value.");
        }
        return SDL_GPUTextureSupportsSampleCount(device, format, sampleCount);
    }

    /// <summary>Inserts an arbitrary string label into the command buffer callstream.</summary>

    /// <param name="command_buffer">a command buffer.</param>
    /// <param name="text">a UTF-8 string constant to insert as the label.</param>
    /// <remarks>
    /// Useful for debugging.
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// </remarks>

    public static void InsertGPUDebugLabel(nint commandBuffer, string text) {
        if (commandBuffer == nint.Zero) {
            throw new ArgumentNullException(nameof(commandBuffer), "Command buffer cannot be null.");
        }
        SDL_InsertGPUDebugLabel(commandBuffer, text);
    }

    /// <summary>Maps a transfer buffer into application address space.</summary>

    /// <param name="device">a GPU context.</param>
    /// <param name="transfer_buffer">a transfer buffer.</param>
    /// <param name="cycle">if <see langword="true" />, cycles the transfer buffer if it is already bound.</param>
    /// <remarks>
    /// You must unmap the transfer buffer before encoding upload commands. The
    /// memory is owned by the graphics driver - do NOT call SDL_free()
    /// on the returned pointer.
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>(void *) Returns the address of the mapped transfer buffer memory, or <see langword="null" />on failure; call <see cref="GetError()" /> for more information.</returns>

    public static nint MapGPUTransferBuffer(nint device, nint transferBuffer, bool cycle) {
        if (device == nint.Zero) {
            throw new ArgumentNullException(nameof(device), "Device cannot be null.");
        }
        if (transferBuffer == nint.Zero) {
            throw new ArgumentNullException(nameof(transferBuffer), "Transfer buffer cannot be null.");
        }
        return SDL_MapGPUTransferBuffer(device, transferBuffer, cycle);
    }

    /// <summary>Ends the most-recently pushed debug group.</summary>

    /// <param name="command_buffer">a command buffer.</param>
    /// <remarks>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="PushGPUDebugGroup"/>
    /// </remarks>

    public static void PopGPUDebugGroup(nint commandBuffer) {
        if (commandBuffer == nint.Zero) {
            throw new ArgumentNullException(nameof(commandBuffer), "Command buffer cannot be null.");
        }
        SDL_PopGPUDebugGroup(commandBuffer);
    }

    /// <summary>Pushes data to a uniform slot on the command buffer.</summary>

    /// <param name="command_buffer">a command buffer.</param>
    /// <param name="slot_index">the uniform slot to push data to.</param>
    /// <param name="data">client data to write.</param>
    /// <param name="length">the length of the data to write.</param>
    /// <remarks>
    /// Subsequent draw calls will use this uniform data.
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// </remarks>

    public static void PushGPUComputeUniformData(nint commandBuffer, uint slotIndex, nint data, uint length) {
        if (commandBuffer == nint.Zero) {
            throw new ArgumentNullException(nameof(commandBuffer), "Command buffer cannot be null.");
        }
        SDL_PushGPUComputeUniformData(commandBuffer, slotIndex, data, length);
    }

    /// <summary>Begins a debug group with an arbitary name.</summary>

    /// <param name="command_buffer">a command buffer.</param>
    /// <param name="name">a UTF-8 string constant that names the group.</param>
    /// <remarks>
    /// Used for denoting groups of calls when viewing the command buffer
    /// callstream in a graphics debugging tool.
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="PopGPUDebugGroup"/>
    /// </remarks>

    public static void PushGPUDebugGroup(nint commandBuffer, string name) {
        if (commandBuffer == nint.Zero) {
            throw new ArgumentNullException(nameof(commandBuffer), "Command buffer cannot be null.");
        }
        SDL_PushGPUDebugGroup(commandBuffer, name);
    }

    /// <summary>Pushes data to a fragment uniform slot on the command buffer.</summary>

    /// <param name="command_buffer">a command buffer.</param>
    /// <param name="slot_index">the fragment uniform slot to push data to.</param>
    /// <param name="data">client data to write.</param>
    /// <param name="length">the length of the data to write.</param>
    /// <remarks>
    /// Subsequent draw calls will use this uniform data.
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// </remarks>

    public static void PushGPUFragmentUniformData(nint commandBuffer, uint slotIndex, nint data, uint length) {
        if (commandBuffer == nint.Zero) {
            throw new ArgumentNullException(nameof(commandBuffer), "Command buffer cannot be null.");
        }
        SDL_PushGPUFragmentUniformData(commandBuffer, slotIndex, data, length);
    }

    /// <summary>Pushes data to a vertex uniform slot on the command buffer.</summary>

    /// <param name="command_buffer">a command buffer.</param>
    /// <param name="slot_index">the vertex uniform slot to push data to.</param>
    /// <param name="data">client data to write.</param>
    /// <param name="length">the length of the data to write.</param>
    /// <remarks>
    /// Subsequent draw calls will use this uniform data.
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// </remarks>

    public static void PushGPUVertexUniformData(nint commandBuffer, uint slotIndex, nint data, uint length) {
        if (commandBuffer == nint.Zero) {
            throw new ArgumentNullException(nameof(commandBuffer), "Command buffer cannot be null.");
        }
        SDL_PushGPUVertexUniformData(commandBuffer, slotIndex, data, length);
    }

    /// <summary>Checks the status of a fence.</summary>

    /// <param name="device">a GPU context.</param>
    /// <param name="fence">a fence.</param>
    /// <remarks>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="SubmitGPUCommandBufferAndAcquireFence"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> if the fence is signaled, <see langword="false" /> if it is not.</returns>

    public static bool QueryGPUFence(nint device, nint fence) {
        if (device == nint.Zero) {
            throw new ArgumentNullException(nameof(device), "Device cannot be null.");
        }
        if (fence == nint.Zero) {
            throw new ArgumentNullException(nameof(fence), "Fence cannot be null.");
        }
        return SDL_QueryGPUFence(device, fence);
    }

    /// <summary>Frees the given buffer as soon as it is safe to do so.</summary>

    /// <param name="device">a GPU context.</param>
    /// <param name="buffer">a buffer to be destroyed.</param>
    /// <remarks>
    /// You must not reference the buffer after calling this function.
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// </remarks>

    public static void ReleaseGPUBuffer(nint device, nint buffer) {
        if (device == nint.Zero) {
            throw new ArgumentNullException(nameof(device), "Device cannot be null.");
        }
        if (buffer == nint.Zero) {
            throw new ArgumentNullException(nameof(buffer), "Buffer cannot be null.");
        }
        SDL_ReleaseGPUBuffer(device, buffer);
    }

    /// <summary>Frees the given compute pipeline as soon as it is safe to do so.</summary>

    /// <param name="device">a GPU context.</param>
    /// <param name="compute_pipeline">a compute pipeline to be destroyed.</param>
    /// <remarks>
    /// You must not reference the compute pipeline after calling this function.
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// </remarks>

    public static void ReleaseGPUComputePipeline(nint device, nint computePipeline) {
        if (device == nint.Zero) {
            throw new ArgumentNullException(nameof(device), "Device cannot be null.");
        }
        if (computePipeline == nint.Zero) {
            throw new ArgumentNullException(nameof(computePipeline), "Compute pipeline cannot be null.");
        }
        SDL_ReleaseGPUComputePipeline(device, computePipeline);
    }

    /// <summary>Releases a fence obtained from SDL_SubmitGPUCommandBufferAndAcquireFence.</summary>

    /// <param name="device">a GPU context.</param>
    /// <param name="fence">a fence.</param>
    /// <remarks>
    /// You must not reference the fence after calling this function.
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="SubmitGPUCommandBufferAndAcquireFence"/>
    /// </remarks>

    public static void ReleaseGPUFence(nint device, nint fence) {
        if (device == nint.Zero) {
            throw new ArgumentNullException(nameof(device), "Device cannot be null.");
        }
        if (fence == nint.Zero) {
            throw new ArgumentNullException(nameof(fence), "Fence cannot be null.");
        }
        SDL_ReleaseGPUFence(device, fence);
    }

    /// <summary>Frees the given graphics pipeline as soon as it is safe to do so.</summary>

    /// <param name="device">a GPU context.</param>
    /// <param name="graphics_pipeline">a graphics pipeline to be destroyed.</param>
    /// <remarks>
    /// You must not reference the graphics pipeline after calling this function.
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// </remarks>

    public static void ReleaseGPUGraphicsPipeline(nint device, nint graphicsPipeline) {
        if (device == nint.Zero) {
            throw new ArgumentNullException(nameof(device), "Device cannot be null.");
        }
        if (graphicsPipeline == nint.Zero) {
            throw new ArgumentNullException(nameof(graphicsPipeline), "Graphics pipeline cannot be null.");
        }
        SDL_ReleaseGPUGraphicsPipeline(device, graphicsPipeline);
    }

    /// <summary>Frees the given sampler as soon as it is safe to do so.</summary>

    /// <param name="device">a GPU context.</param>
    /// <param name="sampler">a sampler to be destroyed.</param>
    /// <remarks>
    /// You must not reference the sampler after calling this function.
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// </remarks>

    public static void ReleaseGPUSampler(nint device, nint sampler) {
        if (device == nint.Zero) {
            throw new ArgumentNullException(nameof(device), "Device cannot be null.");
        }
        if (sampler == nint.Zero) {
            throw new ArgumentNullException(nameof(sampler), "Sampler cannot be null.");
        }
        SDL_ReleaseGPUSampler(device, sampler);
    }

    /// <summary>Frees the given shader as soon as it is safe to do so.</summary>

    /// <param name="device">a GPU context.</param>
    /// <param name="shader">a shader to be destroyed.</param>
    /// <remarks>
    /// You must not reference the shader after calling this function.
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// </remarks>

    public static void ReleaseGPUShader(nint device, nint shader) {
        if (device == nint.Zero) {
            throw new ArgumentNullException(nameof(device), "Device cannot be null.");
        }
        if (shader == nint.Zero) {
            throw new ArgumentNullException(nameof(shader), "Shader cannot be null.");
        }
        SDL_ReleaseGPUShader(device, shader);
    }

    /// <summary>Frees the given texture as soon as it is safe to do so.</summary>

    /// <param name="device">a GPU context.</param>
    /// <param name="texture">a texture to be destroyed.</param>
    /// <remarks>
    /// You must not reference the texture after calling this function.
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// </remarks>

    public static void ReleaseGPUTexture(nint device, nint texture) {
        if (device == nint.Zero) {
            throw new ArgumentNullException(nameof(device), "Device cannot be null.");
        }
        if (texture == nint.Zero) {
            throw new ArgumentNullException(nameof(texture), "Texture cannot be null.");
        }
        SDL_ReleaseGPUTexture(device, texture);
    }

    /// <summary>Frees the given transfer buffer as soon as it is safe to do so.</summary>

    /// <param name="device">a GPU context.</param>
    /// <param name="transfer_buffer">a transfer buffer to be destroyed.</param>
    /// <remarks>
    /// You must not reference the transfer buffer after calling this function.
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// </remarks>

    public static void ReleaseGPUTransferBuffer(nint device, nint transferBuffer) {
        if (device == nint.Zero) {
            throw new ArgumentNullException(nameof(device), "Device cannot be null.");
        }
        if (transferBuffer == nint.Zero) {
            throw new ArgumentNullException(nameof(transferBuffer), "Transfer buffer cannot be null.");
        }
        SDL_ReleaseGPUTransferBuffer(device, transferBuffer);
    }

    /// <summary>Unclaims a window, destroying its swapchain structure.</summary>

    /// <param name="device">a GPU context.</param>
    /// <param name="window">an SDL_Window that has been claimed.</param>
    /// <remarks>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="ClaimWindowForGPUDevice"/>
    /// </remarks>

    public static void ReleaseWindowFromGPUDevice(nint device, nint window) {
        if (device == nint.Zero) {
            throw new ArgumentNullException(nameof(device), "Device cannot be null.");
        }
        if (window == nint.Zero) {
            throw new ArgumentNullException(nameof(window), "Window cannot be null.");
        }
        SDL_ReleaseWindowFromGPUDevice(device, window);
    }

    /// <summary>Configures the maximum allowed number of frames in flight.</summary>

    /// <param name="device">a GPU context.</param>
    /// <param name="allowed_frames_in_flight">the maximum number of frames that can be pending on the GPU.</param>
    /// <remarks>
    /// The default value when the device is created is 2. This means that after
    /// you have submitted 2 frames for presentation, if the GPU has not finished
    /// working on the first frame,
    /// SDL_AcquireGPUSwapchainTexture() will
    /// fill the swapchain texture pointer with <see langword="null" />, and
    /// SDL_WaitAndAcquireGPUSwapchainTexture()
    /// will block.
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> if successful, <see langword="false" /> on error; call <see cref="GetError()"/> for more information.</returns>

    public static bool SetGPUAllowedFramesInFlight(nint device, uint allowedFramesInFlight) {
        if (device == nint.Zero) {
            throw new ArgumentNullException(nameof(device), "Device cannot be null.");
        }
        return SDL_SetGPUAllowedFramesInFlight(device, allowedFramesInFlight);
    }

    /// <summary>Sets the current blend constants on a command buffer.</summary>

    /// <param name="render_pass">a render pass handle.</param>
    /// <param name="blend_constants">the blend constant color.</param>
    /// <remarks>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GPU_BLENDFACTOR_CONSTANT_COLOR"/>
    /// <seealso cref="GPU_BLENDFACTOR_ONE_MINUS_CONSTANT_COLOR"/>
    /// </remarks>

    public static void SetGPUBlendConstants(nint renderPass, FColor blendConstants) {
        if (renderPass == nint.Zero) {
            throw new ArgumentNullException(nameof(renderPass), "Render pass cannot be null.");
        }
        SDL_SetGPUBlendConstants(renderPass, blendConstants);
    }

    /// <summary>Sets an arbitrary string constant to label a buffer.</summary>

    /// <param name="device">a GPU Context.</param>
    /// <param name="buffer">a buffer to attach the name to.</param>
    /// <param name="text">a UTF-8 string constant to mark as the name of the buffer.</param>
    /// <remarks>
    /// You should use
    /// SDL_PROP_GPU_BUFFER_CREATE_NAME_STRING
    /// with SDL_CreateGPUBuffer instead of this function to
    /// avoid thread safety issues.
    /// <para><strong>Thread Safety:</strong> This function is not thread safe, you must make sure the buffer is notsimultaneously used by any other thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="CreateGPUBuffer"/>
    /// </remarks>

    public static void SetGPUBufferName(nint device, nint buffer, string text) {
        if (device == nint.Zero) {
            throw new ArgumentNullException(nameof(device), "Device cannot be null.");
        }
        if (buffer == nint.Zero) {
            throw new ArgumentNullException(nameof(buffer), "Buffer cannot be null.");
        }
        SDL_SetGPUBufferName(device, buffer, text);
    }

    /// <summary>Sets the current scissor state on a command buffer.</summary>

    /// <param name="render_pass">a render pass handle.</param>
    /// <param name="scissor">the scissor area to set.</param>
    /// <remarks>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// </remarks>

    public static void SetGPUScissor(nint renderPass, in Rect scissor) {
        if (renderPass == nint.Zero) {
            throw new ArgumentNullException(nameof(renderPass), "Render pass cannot be null.");
        }
        SDL_SetGPUScissor(renderPass, scissor);
    }

    /// <summary>Sets the current stencil reference value on a command buffer.</summary>

    /// <param name="render_pass">a render pass handle.</param>
    /// <param name="reference">the stencil reference value to set.</param>
    /// <remarks>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// </remarks>

    public static void SetGPUStencilReference(nint renderPass, byte reference) {
        if (renderPass == nint.Zero) {
            throw new ArgumentNullException(nameof(renderPass), "Render pass cannot be null.");
        }
        SDL_SetGPUStencilReference(renderPass, reference);
    }

    /// <summary>Changes the swapchain parameters for the given claimed window.</summary>

    /// <param name="device">a GPU context.</param>
    /// <param name="window">an SDL_Window that has been claimed.</param>
    /// <param name="swapchain_composition">the desired composition of the swapchain.</param>
    /// <param name="present_mode">the desired present mode for the swapchain.</param>
    /// <remarks>
    /// This function will fail if the requested present mode or swapchain
    /// composition are unsupported by the device. Check if the parameters are
    /// supported via
    /// SDL_WindowSupportsGPUPresentMode /
    /// SDL_WindowSupportsGPUSwapchainComposition
    /// prior to calling this function.
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="WindowSupportsGPUPresentMode"/>
    /// <seealso cref="WindowSupportsGPUSwapchainComposition"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> if successful, <see langword="false" /> on error; call <see cref="GetError()"/> for more information.</returns>

    public static bool SetGPUSwapchainParameters(nint device, nint window, GpuSwapchainComposition swapchainComposition, GpuPresentMode presentMode) {
        if (device == nint.Zero) {
            throw new ArgumentNullException(nameof(device), "Device cannot be null.");
        }
        if (window == nint.Zero) {
            throw new ArgumentNullException(nameof(window), "Window cannot be null.");
        }
        return SDL_SetGPUSwapchainParameters(device, window, swapchainComposition, presentMode);
    }

    /// <summary>Sets an arbitrary string constant to label a texture.</summary>

    /// <param name="device">a GPU Context.</param>
    /// <param name="texture">a texture to attach the name to.</param>
    /// <param name="text">a UTF-8 string constant to mark as the name of the texture.</param>
    /// <remarks>
    /// You should use
    /// SDL_PROP_GPU_TEXTURE_CREATE_NAME_STRING
    /// with SDL_CreateGPUTexture instead of this function
    /// to avoid thread safety issues.
    /// <para><strong>Thread Safety:</strong> This function is not thread safe, you must make sure the texture is notsimultaneously used by any other thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="CreateGPUTexture"/>
    /// </remarks>

    public static void SetGPUTextureName(nint device, nint texture, string text) {
        if (device == nint.Zero) {
            throw new ArgumentNullException(nameof(device), "Device cannot be null.");
        }
        if (texture == nint.Zero) {
            throw new ArgumentNullException(nameof(texture), "Texture cannot be null.");
        }
        SDL_SetGPUTextureName(device, texture, text);
    }

    /// <summary>Sets the current viewport state on a command buffer.</summary>

    /// <param name="render_pass">a render pass handle.</param>
    /// <param name="viewport">the viewport to set.</param>
    /// <remarks>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// </remarks>

    public static void SetGPUViewport(nint renderPass, in GpuViewport viewport) {
        if (renderPass == nint.Zero) {
            throw new ArgumentNullException(nameof(renderPass), "Render pass cannot be null.");
        }
        SDL_SetGPUViewport(renderPass, viewport);
    }

    /// <summary>Submits a command buffer so its commands can be processed on the GPU.</summary>

    /// <param name="command_buffer">a command buffer.</param>
    /// <remarks>
    /// It is invalid to use the command buffer after this is called.
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="AcquireGPUCommandBuffer"/>
    /// <seealso cref="WaitAndAcquireGPUSwapchainTexture"/>
    /// <seealso cref="AcquireGPUSwapchainTexture"/>
    /// <seealso cref="SubmitGPUCommandBufferAndAcquireFence"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success, <see langword="false" /> on failure; call <see cref="GetError()"/> for more information.</returns>

    public static bool SubmitGPUCommandBuffer(nint commandBuffer) {
        if (commandBuffer == nint.Zero) {
            throw new ArgumentNullException(nameof(commandBuffer), "Command buffer cannot be null.");
        }
        return SDL_SubmitGPUCommandBuffer(commandBuffer);
    }

    /// <summary>Submits a command buffer so its commands can be processed on the GPU, and acquires a fence associated with the command buffer.</summary>

    /// <param name="command_buffer">a command buffer.</param>
    /// <remarks>
    /// You must release this fence when it is no longer needed or it will cause a
    /// leak. It is invalid to use the command buffer after this is called.
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="AcquireGPUCommandBuffer"/>
    /// <seealso cref="WaitAndAcquireGPUSwapchainTexture"/>
    /// <seealso cref="AcquireGPUSwapchainTexture"/>
    /// <seealso cref="SubmitGPUCommandBuffer"/>
    /// <seealso cref="ReleaseGPUFence"/>
    /// </remarks>
    /// <returns>(SDL_GPUFence *) Returns a fence associated with thecommand buffer, or <see langword="null" /> on failure; call <see cref="GetError()" /> for more information.</returns>

    public static nint SubmitGPUCommandBufferAndAcquireFence(nint commandBuffer) {
        if (commandBuffer == nint.Zero) {
            throw new ArgumentNullException(nameof(commandBuffer), "Command buffer cannot be null.");
        }
        return SDL_SubmitGPUCommandBufferAndAcquireFence(commandBuffer);
    }

    /// <summary>Unmaps a previously mapped transfer buffer.</summary>

    /// <param name="device">a GPU context.</param>
    /// <param name="transfer_buffer">a previously mapped transfer buffer.</param>
    /// <remarks>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// </remarks>

    public static void UnmapGPUTransferBuffer(nint device, nint transferBuffer) {
        if (device == nint.Zero) {
            throw new ArgumentNullException(nameof(device), "Device cannot be null.");
        }
        if (transferBuffer == nint.Zero) {
            throw new ArgumentNullException(nameof(transferBuffer), "Transfer buffer cannot be null.");
        }
        SDL_UnmapGPUTransferBuffer(device, transferBuffer);
    }

    /// <summary>Uploads data from a transfer buffer to a buffer.</summary>

    /// <param name="copy_pass">a copy pass handle.</param>
    /// <param name="source">the source transfer buffer with offset.</param>
    /// <param name="destination">the destination buffer with offset and size.</param>
    /// <param name="cycle">if <see langword="true" />, cycles the buffer if it is already bound, otherwise overwrites the data.</param>
    /// <remarks>
    /// The upload occurs on the GPU timeline. You may assume that the upload has
    /// finished in subsequent commands.
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// </remarks>

    public static void UploadToGPUBuffer(nint copyPass, in GpuTransferBufferLocation source,
            in GpuBufferRegion destination, bool cycle) {
        if (copyPass == nint.Zero) {
            throw new ArgumentNullException(nameof(copyPass), "Copy pass cannot be null.");
        }
        SDL_UploadToGPUBuffer(copyPass, source, destination, cycle);
    }

    /// <summary>Uploads data from a transfer buffer to a texture.</summary>

    /// <param name="copy_pass">a copy pass handle.</param>
    /// <param name="source">the source transfer buffer with image layout information.</param>
    /// <param name="destination">the destination texture region.</param>
    /// <param name="cycle">if <see langword="true" />, cycles the texture if the texture is bound, otherwise overwrites the data.</param>
    /// <remarks>
    /// The upload occurs on the GPU timeline. You may assume that the upload has
    /// finished in subsequent commands.
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// </remarks>

    public static void UploadToGPUTexture(nint copyPass, in GpuTextureTransferInfo source,
            in GpuTextureRegion destination, bool cycle) {
        if (copyPass == nint.Zero) {
            throw new ArgumentNullException(nameof(copyPass), "Copy pass cannot be null.");
        }
        SDL_UploadToGPUTexture(copyPass, source, destination, cycle);
    }

    /// <summary>Blocks the thread until a swapchain texture is available to be acquired, and then acquires it.</summary>

    /// <param name="command_buffer">a command buffer.</param>
    /// <param name="window">a window that has been claimed.</param>
    /// <param name="swapchain_texture">a pointer filled in with a swapchain texture handle.</param>
    /// <param name="swapchain_texture_width">a pointer filled in with the swapchain texture width, discarded.</param>
    /// <param name="swapchain_texture_height">a pointer filled in with the swapchain texture height, discarded.</param>
    /// <remarks>
    /// When a swapchain texture is acquired on a command buffer, it will
    /// automatically be submitted for presentation when the command buffer is
    /// submitted. The swapchain texture should only be referenced by the command
    /// buffer used to acquire it. It is an error to call
    /// SDL_CancelGPUCommandBuffer() after a
    /// swapchain texture is acquired.
    /// <para><strong>Thread Safety:</strong> This function should only be called from the thread that created thewindow.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="SubmitGPUCommandBuffer"/>
    /// <seealso cref="SubmitGPUCommandBufferAndAcquireFence"/>
    /// <seealso cref="AcquireGPUSwapchainTexture"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success, <see langword="false" /> on error; call <see cref="GetError()"/> for more information.</returns>

    public static bool WaitAndAcquireGPUSwapchainTexture(nint commandBuffer, nint window,
            out nint swapchainTexture, out uint swapchainTextureWidth, out uint swapchainTextureHeight) {
        if (commandBuffer == nint.Zero) {
            throw new ArgumentNullException(nameof(commandBuffer), "Command buffer cannot be null.");
        }
        if (window == nint.Zero) {
            throw new ArgumentNullException(nameof(window), "Window cannot be null.");
        }
        return SDL_WaitAndAcquireGPUSwapchainTexture(commandBuffer, window, out swapchainTexture, out swapchainTextureWidth, out swapchainTextureHeight);
    }

    /// <summary>Blocks the thread until the given fences are signaled.</summary>

    /// <param name="device">a GPU context.</param>
    /// <param name="wait_all">if 0, wait for any fence to be signaled, if 1, wait for all fences to be signaled.</param>
    /// <param name="fences">an array of fences to wait on.</param>
    /// <param name="num_fences">the number of fences in the fences array.</param>
    /// <remarks>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="SubmitGPUCommandBufferAndAcquireFence"/>
    /// <seealso cref="WaitForGPUIdle"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success, <see langword="false" /> on failure; call <see cref="GetError()"/> for more information.</returns>

    public static bool WaitForGPUFences(nint device, bool waitAll, Span<nint> fences, uint numFences) {
        if (device == nint.Zero) {
            throw new ArgumentNullException(nameof(device), "Device cannot be null.");
        }
        return SDL_WaitForGPUFences(device, waitAll, fences, numFences);
    }

    /// <summary>Blocks the thread until the GPU is completely idle.</summary>

    /// <param name="device">a GPU context.</param>
    /// <remarks>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="WaitForGPUFences"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success, <see langword="false" /> on failure; call <see cref="GetError()"/> for more information.</returns>

    public static bool WaitForGPUIdle(nint device) {
        if (device == nint.Zero) {
            throw new ArgumentNullException(nameof(device), "Device cannot be null.");
        }
        return SDL_WaitForGPUIdle(device);
    }

    /// <summary>Blocks the thread until a swapchain texture is available to be acquired.</summary>

    /// <param name="device">a GPU context.</param>
    /// <param name="window">a window that has been claimed.</param>
    /// <remarks>
    /// <para><strong>Thread Safety:</strong> This function should only be called from the thread that created thewindow.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="AcquireGPUSwapchainTexture"/>
    /// <seealso cref="WaitAndAcquireGPUSwapchainTexture"/>
    /// <seealso cref="SetGPUAllowedFramesInFlight"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success, <see langword="false" /> on failure; call <see cref="GetError()"/> for more information.</returns>

    public static bool WaitForGPUSwapchain(nint device, nint window) {
        if (device == nint.Zero) {
            throw new ArgumentNullException(nameof(device), "Device cannot be null.");
        }
        if (window == nint.Zero) {
            throw new ArgumentNullException(nameof(window), "Window cannot be null.");
        }
        return SDL_WaitForGPUSwapchain(device, window);
    }

    /// <summary>Determines whether a presentation mode is supported by the window.</summary>

    /// <param name="device">a GPU context.</param>
    /// <param name="window">an SDL_Window.</param>
    /// <param name="present_mode">the presentation mode to check.</param>
    /// <remarks>
    /// The window must be claimed before calling this function.
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="ClaimWindowForGPUDevice"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> if supported, <see langword="false" /> if unsupported.</returns>

    public static bool WindowSupportsGPUPresentMode(nint device, nint window, GpuPresentMode presentMode) {
        if (device == nint.Zero) {
            throw new ArgumentNullException(nameof(device), "Device cannot be null.");
        }
        if (window == nint.Zero) {
            throw new ArgumentNullException(nameof(window), "Window cannot be null.");
        }
        return SDL_WindowSupportsGPUPresentMode(device, window, presentMode);
    }

    /// <summary>Determines whether a swapchain composition is supported by the window.</summary>

    /// <param name="device">a GPU context.</param>
    /// <param name="window">an SDL_Window.</param>
    /// <param name="swapchain_composition">the swapchain composition to check.</param>
    /// <remarks>
    /// The window must be claimed before calling this function.
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="ClaimWindowForGPUDevice"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> if supported, <see langword="false" /> if unsupported.</returns>

    public static bool WindowSupportsGPUSwapchainComposition(nint device, nint window, GpuSwapchainComposition swapchainComposition) {
        if (device == nint.Zero) {
            throw new ArgumentNullException(nameof(device), "Device cannot be null.");
        }
        if (window == nint.Zero) {
            throw new ArgumentNullException(nameof(window), "Window cannot be null.");
        }
        return SDL_WindowSupportsGPUSwapchainComposition(device, window, swapchainComposition);
    }

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_AcquireGPUCommandBuffer(nint device);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_AcquireGPUSwapchainTexture(nint commandBuffer, nint window,
        out nint swapchainTexture, out uint swapchainTextureWidth, out uint swapchainTextureHeight);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_BeginGPUComputePass(nint commandBuffer,
        Span<GpuStorageTextureReadWriteBinding> storageTextureBindings, uint numStorageTextureBindings,
        Span<GpuStorageBufferReadWriteBinding> storageBufferBindings, uint numStorageBufferBindings);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_BeginGPUCopyPass(nint commandBuffer);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_BeginGPURenderPass(nint commandBuffer,
        Span<GpuColorTargetInfo> colorTargetInfos, uint numColorTargets,
        in GpuDepthStencilTargetInfo depthStencilTargetInfo);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_BindGPUComputePipeline(nint computePass, nint computePipeline);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_BindGPUComputeSamplers(nint computePass, uint firstSlot,
        Span<GpuTextureSamplerBinding> textureSamplerBindings, uint numBindings);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_BindGPUComputeStorageBuffers(nint computePass, uint firstSlot,
        Span<nint> storageBuffers, uint numBindings);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_BindGPUComputeStorageTextures(nint computePass, uint firstSlot,
        Span<nint> storageTextures, uint numBindings);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_BindGPUFragmentSamplers(nint renderPass, uint firstSlot,
        Span<GpuTextureSamplerBinding> textureSamplerBindings, uint numBindings);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_BindGPUFragmentStorageBuffers(nint renderPass, uint firstSlot,
        Span<nint> storageBuffers, uint numBindings);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_BindGPUFragmentStorageTextures(nint renderPass, uint firstSlot,
        Span<nint> storageTextures, uint numBindings);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_BindGPUGraphicsPipeline(nint renderPass, nint graphicsPipeline);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_BindGPUIndexBuffer(nint renderPass, in GpuBufferBinding binding,
        GpuIndexElementSize indexElementSize);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_BindGPUVertexBuffers(nint renderPass, uint firstSlot,
        Span<GpuBufferBinding> bindings, uint numBindings);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_BindGPUVertexSamplers(nint renderPass, uint firstSlot,
        Span<GpuTextureSamplerBinding> textureSamplerBindings, uint numBindings);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_BindGPUVertexStorageBuffers(nint renderPass, uint firstSlot,
        Span<nint> storageBuffers, uint numBindings);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_BindGPUVertexStorageTextures(nint renderPass, uint firstSlot,
        Span<nint> storageTextures, uint numBindings);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_BlitGPUTexture(nint commandBuffer, in GpuBlitInfo info);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial uint SDL_CalculateGPUTextureFormatSize(GpuTextureFormat format, uint width, uint height,
        uint depthOrLayerCount);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_CancelGPUCommandBuffer(nint commandBuffer);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_ClaimWindowForGPUDevice(nint device, nint window);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_CopyGPUBufferToBuffer(nint copyPass, in GpuBufferLocation source,
        in GpuBufferLocation destination, uint size, SdlBool cycle);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_CopyGPUTextureToTexture(nint copyPass, in GpuTextureLocation source,
        in GpuTextureLocation destination, uint w, uint h, uint d, SdlBool cycle);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_CreateGPUBuffer(nint device, in GpuBufferCreateInfo createinfo);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_CreateGPUComputePipeline(nint device,
        in GpuComputePipelineCreateInfo createinfo);

    [LibraryImport(NativeLibName, StringMarshalling = marshalling)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_CreateGPUDevice(GpuShaderFormat formatFlags, SdlBool debugMode, string name);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_CreateGPUDeviceWithProperties(uint props);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_CreateGPUGraphicsPipeline(nint device,
        in GpuGraphicsPipelineCreateInfo createinfo);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_CreateGPUSampler(nint device, in GpuSamplerCreateInfo createinfo);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_CreateGPUShader(nint device, in GpuShaderCreateInfo createinfo);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_CreateGPUTexture(nint device, in GpuTextureCreateInfo createinfo);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_CreateGPUTransferBuffer(nint device,
        in GpuTransferBufferCreateInfo createinfo);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_DestroyGPUDevice(nint device);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_DispatchGPUCompute(nint computePass, uint groupcountX, uint groupcountY,
        uint groupcountZ);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_DispatchGPUComputeIndirect(nint computePass, nint buffer, uint offset);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_DownloadFromGPUBuffer(nint copyPass, in GpuBufferRegion source,
        in GpuTransferBufferLocation destination);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_DownloadFromGPUTexture(nint copyPass, in GpuTextureRegion source,
        in GpuTextureTransferInfo destination);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_DrawGPUIndexedPrimitives(nint renderPass, uint numIndices, uint numInstances,
        uint firstIndex, int vertexOffset, uint firstInstance);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_DrawGPUIndexedPrimitivesIndirect(nint renderPass, nint buffer, uint offset,
        uint drawCount);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_DrawGPUPrimitives(nint renderPass, uint numVertices, uint numInstances,
        uint firstVertex, uint firstInstance);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_DrawGPUPrimitivesIndirect(nint renderPass, nint buffer, uint offset,
        uint drawCount);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_EndGPUComputePass(nint computePass);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_EndGPUCopyPass(nint copyPass);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_EndGPURenderPass(nint renderPass);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_GenerateMipmapsForGPUTexture(nint commandBuffer, nint texture);

    [LibraryImport(NativeLibName, StringMarshalling = marshalling)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(OwnedStringMarshaller))]
    private static partial string SDL_GetGPUDeviceDriver(nint device);

    [LibraryImport(NativeLibName, StringMarshalling = marshalling)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(OwnedStringMarshaller))]
    private static partial string SDL_GetGPUDriver(int index);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial GpuShaderFormat SDL_GetGPUShaderFormats(nint device);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial GpuTextureFormat SDL_GetGPUSwapchainTextureFormat(nint device, nint window);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_GetNumGPUDrivers();

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_GPUSupportsProperties(uint props);

    [LibraryImport(NativeLibName, StringMarshalling = marshalling)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_GPUSupportsShaderFormats(GpuShaderFormat formatFlags, string name);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial uint SDL_GPUTextureFormatTexelBlockSize(GpuTextureFormat format);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_GPUTextureSupportsFormat(nint device, GpuTextureFormat format,
        GpuTextureType type, GpuTextureUsageFlags usage);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_GPUTextureSupportsSampleCount(nint device, GpuTextureFormat format,
        GpuSampleCount sampleCount);

    [LibraryImport(NativeLibName, StringMarshalling = marshalling)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_InsertGPUDebugLabel(nint commandBuffer, string text);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_MapGPUTransferBuffer(nint device, nint transferBuffer, SdlBool cycle);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_PopGPUDebugGroup(nint commandBuffer);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_PushGPUComputeUniformData(nint commandBuffer, uint slotIndex, nint data,
        uint length);

    [LibraryImport(NativeLibName, StringMarshalling = marshalling)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_PushGPUDebugGroup(nint commandBuffer, string name);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_PushGPUFragmentUniformData(nint commandBuffer, uint slotIndex, nint data,
        uint length);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_PushGPUVertexUniformData(nint commandBuffer, uint slotIndex, nint data,
        uint length);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_QueryGPUFence(nint device, nint fence);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_ReleaseGPUBuffer(nint device, nint buffer);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_ReleaseGPUComputePipeline(nint device, nint computePipeline);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_ReleaseGPUFence(nint device, nint fence);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_ReleaseGPUGraphicsPipeline(nint device, nint graphicsPipeline);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_ReleaseGPUSampler(nint device, nint sampler);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_ReleaseGPUShader(nint device, nint shader);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_ReleaseGPUTexture(nint device, nint texture);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_ReleaseGPUTransferBuffer(nint device, nint transferBuffer);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_ReleaseWindowFromGPUDevice(nint device, nint window);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_SetGPUAllowedFramesInFlight(nint device, uint allowedFramesInFlight);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_SetGPUBlendConstants(nint renderPass, FColor blendConstants);

    [LibraryImport(NativeLibName, StringMarshalling = marshalling)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_SetGPUBufferName(nint device, nint buffer, string text);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_SetGPUScissor(nint renderPass, in Rect scissor);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_SetGPUStencilReference(nint renderPass, byte reference);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_SetGPUSwapchainParameters(nint device, nint window,
        GpuSwapchainComposition swapchainComposition, GpuPresentMode presentMode);

    [LibraryImport(NativeLibName, StringMarshalling = marshalling)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_SetGPUTextureName(nint device, nint texture, string text);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_SetGPUViewport(nint renderPass, in GpuViewport viewport);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_SubmitGPUCommandBuffer(nint commandBuffer);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_SubmitGPUCommandBufferAndAcquireFence(nint commandBuffer);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_UnmapGPUTransferBuffer(nint device, nint transferBuffer);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_UploadToGPUBuffer(nint copyPass, in GpuTransferBufferLocation source,
        in GpuBufferRegion destination, SdlBool cycle);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_UploadToGPUTexture(nint copyPass, in GpuTextureTransferInfo source,
        in GpuTextureRegion destination, SdlBool cycle);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_WaitAndAcquireGPUSwapchainTexture(nint commandBuffer, nint window,
        out nint swapchainTexture, out uint swapchainTextureWidth, out uint swapchainTextureHeight);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_WaitForGPUFences(nint device, SdlBool waitAll, Span<nint> fences,
        uint numFences);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_WaitForGPUIdle(nint device);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_WaitForGPUSwapchain(nint device, nint window);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_WindowSupportsGPUPresentMode(nint device, nint window,
        GpuPresentMode presentMode);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_WindowSupportsGPUSwapchainComposition(nint device, nint window,
        GpuSwapchainComposition swapchainComposition);
}