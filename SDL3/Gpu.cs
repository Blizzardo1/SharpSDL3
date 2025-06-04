using SharpSDL3.Enums;
using SharpSDL3.Structs;
using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

namespace SharpSDL3;

public static partial class Sdl {

    public static nint AcquireGPUCommandBuffer(nint device) {
        if (device == nint.Zero) {
            throw new ArgumentNullException(nameof(device), "Device cannot be null.");
        }
        return SDL_AcquireGPUCommandBuffer(device);
    }

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

    public static nint BeginGPUComputePass(nint commandBuffer,
        Span<GpuStorageTextureReadWriteBinding> storageTextureBindings, uint numStorageTextureBindings,
        Span<GpuStorageBufferReadWriteBinding> storageBufferBindings, uint numStorageBufferBindings) {
        if (commandBuffer == nint.Zero) {
            throw new ArgumentNullException(nameof(commandBuffer), "Command buffer cannot be null.");
        }
        return SDL_BeginGPUComputePass(commandBuffer, storageTextureBindings, numStorageTextureBindings,
            storageBufferBindings, numStorageBufferBindings);
    }

    public static nint BeginGPUCopyPass(nint commandBuffer) {
        if (commandBuffer == nint.Zero) {
            throw new ArgumentNullException(nameof(commandBuffer), "Command buffer cannot be null.");
        }
        return SDL_BeginGPUCopyPass(commandBuffer);
    }

    public static nint BeginGPURenderPass(nint commandBuffer, Span<GpuColorTargetInfo> colorTargetInfos,
        uint numColorTargets, in GpuDepthStencilTargetInfo depthStencilTargetInfo) {
        if (commandBuffer == nint.Zero) {
            throw new ArgumentNullException(nameof(commandBuffer), "Command buffer cannot be null.");
        }
        return SDL_BeginGPURenderPass(commandBuffer, colorTargetInfos, numColorTargets, depthStencilTargetInfo);
    }

    public static void BindGPUComputePipeline(nint computePass, nint computePipeline) {
        if (computePass == nint.Zero) {
            throw new ArgumentNullException(nameof(computePass), "Compute pass cannot be null.");
        }
        SDL_BindGPUComputePipeline(computePass, computePipeline);
    }

    public static void BindGPUComputeSamplers(nint computePass, uint firstSlot,
        Span<GpuTextureSamplerBinding> textureSamplerBindings, uint numBindings) {
        if (computePass == nint.Zero) {
            throw new ArgumentNullException(nameof(computePass), "Compute pass cannot be null.");
        }
        SDL_BindGPUComputeSamplers(computePass, firstSlot, textureSamplerBindings, numBindings);
    }

    public static void BindGPUComputeStorageBuffers(nint computePass, uint firstSlot,
        Span<nint> storageBuffers, uint numBindings) {
        if (computePass == nint.Zero) {
            throw new ArgumentNullException(nameof(computePass), "Compute pass cannot be null.");
        }
        SDL_BindGPUComputeStorageBuffers(computePass, firstSlot, storageBuffers, numBindings);
    }

    public static void BindGPUComputeStorageTextures(nint computePass, uint firstSlot,
        Span<nint> storageTextures, uint numBindings) {
        if (computePass == nint.Zero) {
            throw new ArgumentNullException(nameof(computePass), "Compute pass cannot be null.");
        }
        SDL_BindGPUComputeStorageTextures(computePass, firstSlot, storageTextures, numBindings);
    }

    public static void BindGPUFragmentSamplers(nint renderPass, uint firstSlot,
        Span<GpuTextureSamplerBinding> textureSamplerBindings, uint numBindings) {
        if (renderPass == nint.Zero) {
            throw new ArgumentNullException(nameof(renderPass), "Render pass cannot be null.");
        }
        SDL_BindGPUFragmentSamplers(renderPass, firstSlot, textureSamplerBindings, numBindings);
    }

    public static void BindGPUFragmentStorageBuffers(nint renderPass, uint firstSlot,
        Span<nint> storageBuffers, uint numBindings) {
        if (renderPass == nint.Zero) {
            throw new ArgumentNullException(nameof(renderPass), "Render pass cannot be null.");
        }
        SDL_BindGPUFragmentStorageBuffers(renderPass, firstSlot, storageBuffers, numBindings);
    }

    public static void BindGPUFragmentStorageTextures(nint renderPass, uint firstSlot,
        Span<nint> storageTextures, uint numBindings) {
        if (renderPass == nint.Zero) {
            throw new ArgumentNullException(nameof(renderPass), "Render pass cannot be null.");
        }
        SDL_BindGPUFragmentStorageTextures(renderPass, firstSlot, storageTextures, numBindings);
    }

    public static void BindGPUGraphicsPipeline(nint renderPass, nint graphicsPipeline) {
        if (renderPass == nint.Zero) {
            throw new ArgumentNullException(nameof(renderPass), "Render pass cannot be null.");
        }
        SDL_BindGPUGraphicsPipeline(renderPass, graphicsPipeline);
    }

    public static void BindGPUIndexBuffer(nint renderPass, in GpuBufferBinding binding,
        GpuIndexElementSize indexElementSize) {
        if (renderPass == nint.Zero) {
            throw new ArgumentNullException(nameof(renderPass), "Render pass cannot be null.");
        }
        SDL_BindGPUIndexBuffer(renderPass, binding, indexElementSize);
    }

    public static void BindGPUVertexBuffers(nint renderPass, uint firstSlot,
        Span<GpuBufferBinding> bindings, uint numBindings) {
        if (renderPass == nint.Zero) {
            throw new ArgumentNullException(nameof(renderPass), "Render pass cannot be null.");
        }
        SDL_BindGPUVertexBuffers(renderPass, firstSlot, bindings, numBindings);
    }

    public static void BindGPUVertexSamplers(nint renderPass, uint firstSlot,
        Span<GpuTextureSamplerBinding> textureSamplerBindings, uint numBindings) {
        if (renderPass == nint.Zero) {
            throw new ArgumentNullException(nameof(renderPass), "Render pass cannot be null.");
        }
        SDL_BindGPUVertexSamplers(renderPass, firstSlot, textureSamplerBindings, numBindings);
    }

    public static void BindGPUVertexStorageBuffers(nint renderPass, uint firstSlot,
        Span<nint> storageBuffers, uint numBindings) {
        if (renderPass == nint.Zero) {
            throw new ArgumentNullException(nameof(renderPass), "Render pass cannot be null.");
        }
        SDL_BindGPUVertexStorageBuffers(renderPass, firstSlot, storageBuffers, numBindings);
    }

    public static void BindGPUVertexStorageTextures(nint renderPass, uint firstSlot,
        Span<nint> storageTextures, uint numBindings) {
        if (renderPass == nint.Zero) {
            throw new ArgumentNullException(nameof(renderPass), "Render pass cannot be null.");
        }
        SDL_BindGPUVertexStorageTextures(renderPass, firstSlot, storageTextures, numBindings);
    }

    public static void BlitGPUTexture(nint commandBuffer, in GpuBlitInfo info) {
        if (commandBuffer == nint.Zero) {
            throw new ArgumentNullException(nameof(commandBuffer), "Command buffer cannot be null.");
        }
        SDL_BlitGPUTexture(commandBuffer, info);
    }

    public static uint CalculateGPUTextureFormatSize(GpuTextureFormat format, uint width, uint height, uint depthOrLayerCount) {
        if (!Enum.IsDefined(format)) {
            throw new ArgumentOutOfRangeException(nameof(format), "Invalid GpuTextureFormat value.");
        }
        return SDL_CalculateGPUTextureFormatSize(format, width, height, depthOrLayerCount);
    }

    public static bool CancelGPUCommandBuffer(nint commandBuffer) {
        if (commandBuffer == nint.Zero) {
            throw new ArgumentNullException(nameof(commandBuffer), "Command buffer cannot be null.");
        }
        return SDL_CancelGPUCommandBuffer(commandBuffer);
    }

    public static bool ClaimWindowForGPUDevice(nint device, nint window) {
        if (device == nint.Zero) {
            throw new ArgumentNullException(nameof(device), "Device cannot be null.");
        }
        if (window == nint.Zero) {
            throw new ArgumentNullException(nameof(window), "Window cannot be null.");
        }
        return SDL_ClaimWindowForGPUDevice(device, window);
    }

    public static void CopyGPUBufferToBuffer(nint copyPass, in GpuBufferLocation source,
        in GpuBufferLocation destination, uint size, bool cycle) {
        if (copyPass == nint.Zero) {
            throw new ArgumentNullException(nameof(copyPass), "Copy pass cannot be null.");
        }
        SDL_CopyGPUBufferToBuffer(copyPass, source, destination, size, cycle);
    }

    public static void CopyGPUTextureToTexture(nint copyPass, in GpuTextureLocation source,
        in GpuTextureLocation destination, uint w, uint h, uint d, bool cycle) {
        if (copyPass == nint.Zero) {
            throw new ArgumentNullException(nameof(copyPass), "Copy pass cannot be null.");
        }
        SDL_CopyGPUTextureToTexture(copyPass, source, destination, w, h, d, cycle);
    }

    public static nint CreateGPUBuffer(nint device, in GpuBufferCreateInfo createinfo) {
        if (device == nint.Zero) {
            throw new ArgumentNullException(nameof(device), "Device cannot be null.");
        }
        return SDL_CreateGPUBuffer(device, createinfo);
    }

    public static nint CreateGPUComputePipeline(nint device, in GpuComputePipelineCreateInfo createinfo) {
        if (device == nint.Zero) {
            throw new ArgumentNullException(nameof(device), "Device cannot be null.");
        }
        return SDL_CreateGPUComputePipeline(device, createinfo);
    }

    public static nint CreateGPUDevice(GpuShaderFormat formatFlags, bool debugMode, string name) {
        if (!Enum.IsDefined(formatFlags)) {
            throw new ArgumentOutOfRangeException(nameof(formatFlags), "Invalid GpuShaderFormat value.");
        }
        if (string.IsNullOrEmpty(name)) {
            throw new ArgumentException("Name cannot be null or empty.", nameof(name));
        }
        return SDL_CreateGPUDevice(formatFlags, debugMode, name);
    }

    public static nint CreateGPUDeviceWithProperties(uint props) {
        return SDL_CreateGPUDeviceWithProperties(props);
    }

    public static nint CreateGPUGraphicsPipeline(nint device, in GpuGraphicsPipelineCreateInfo createinfo) {
        if (device == nint.Zero) {
            throw new ArgumentNullException(nameof(device), "Device cannot be null.");
        }
        return SDL_CreateGPUGraphicsPipeline(device, createinfo);
    }

    public static nint CreateGPUSampler(nint device, in GpuSamplerCreateInfo createinfo) {
        if (device == nint.Zero) {
            throw new ArgumentNullException(nameof(device), "Device cannot be null.");
        }
        return SDL_CreateGPUSampler(device, createinfo);
    }

    public static nint CreateGPUShader(nint device, in GpuShaderCreateInfo createinfo) {
        if (device == nint.Zero) {
            throw new ArgumentNullException(nameof(device), "Device cannot be null.");
        }
        return SDL_CreateGPUShader(device, createinfo);
    }

    public static nint CreateGPUTexture(nint device, in GpuTextureCreateInfo createinfo) {
        if (device == nint.Zero) {
            throw new ArgumentNullException(nameof(device), "Device cannot be null.");
        }
        return SDL_CreateGPUTexture(device, createinfo);
    }

    public static nint CreateGPUTransferBuffer(nint device, in GpuTransferBufferCreateInfo createinfo) {
        if (device == nint.Zero) {
            throw new ArgumentNullException(nameof(device), "Device cannot be null.");
        }
        return SDL_CreateGPUTransferBuffer(device, createinfo);
    }

    public static void DestroyGPUDevice(nint device) {
        if (device == nint.Zero) {
            throw new ArgumentNullException(nameof(device), "Device cannot be null.");
        }
        SDL_DestroyGPUDevice(device);
    }

    public static void DispatchGPUCompute(nint computePass, uint groupcountX, uint groupcountY, uint groupcountZ) {
        if (computePass == nint.Zero) {
            throw new ArgumentNullException(nameof(computePass), "Compute pass cannot be null.");
        }
        SDL_DispatchGPUCompute(computePass, groupcountX, groupcountY, groupcountZ);
    }

    public static void DispatchGPUComputeIndirect(nint computePass, nint buffer, uint offset) {
        if (computePass == nint.Zero) {
            throw new ArgumentNullException(nameof(computePass), "Compute pass cannot be null.");
        }
        SDL_DispatchGPUComputeIndirect(computePass, buffer, offset);
    }

    public static void DownloadFromGPUBuffer(nint copyPass, in GpuBufferRegion source,
        in GpuTransferBufferLocation destination) {
        if (copyPass == nint.Zero) {
            throw new ArgumentNullException(nameof(copyPass), "Copy pass cannot be null.");
        }
        SDL_DownloadFromGPUBuffer(copyPass, source, destination);
    }

    public static void DownloadFromGPUTexture(nint copyPass, in GpuTextureRegion source,
        in GpuTextureTransferInfo destination) {
        if (copyPass == nint.Zero) {
            throw new ArgumentNullException(nameof(copyPass), "Copy pass cannot be null.");
        }
        SDL_DownloadFromGPUTexture(copyPass, source, destination);
    }

    public static void DrawGPUIndexedPrimitives(nint renderPass, uint numIndices, uint numInstances,
        uint firstIndex, int vertexOffset, uint firstInstance) {
        if (renderPass == nint.Zero) {
            throw new ArgumentNullException(nameof(renderPass), "Render pass cannot be null.");
        }
        SDL_DrawGPUIndexedPrimitives(renderPass, numIndices, numInstances, firstIndex, vertexOffset, firstInstance);
    }

    public static void DrawGPUIndexedPrimitivesIndirect(nint renderPass, nint buffer, uint offset, uint drawCount) {
        if (renderPass == nint.Zero) {
            throw new ArgumentNullException(nameof(renderPass), "Render pass cannot be null.");
        }
        SDL_DrawGPUIndexedPrimitivesIndirect(renderPass, buffer, offset, drawCount);
    }

    public static void DrawGPUPrimitives(nint renderPass, uint numVertices, uint numInstances,
        uint firstVertex, uint firstInstance) {
        if (renderPass == nint.Zero) {
            throw new ArgumentNullException(nameof(renderPass), "Render pass cannot be null.");
        }
        SDL_DrawGPUPrimitives(renderPass, numVertices, numInstances, firstVertex, firstInstance);
    }

    public static void DrawGPUPrimitivesIndirect(nint renderPass, nint buffer, uint offset, uint drawCount) {
        if (renderPass == nint.Zero) {
            throw new ArgumentNullException(nameof(renderPass), "Render pass cannot be null.");
        }
        SDL_DrawGPUPrimitivesIndirect(renderPass, buffer, offset, drawCount);
    }

    public static void EndGPUComputePass(nint computePass) {
        if (computePass == nint.Zero) {
            throw new ArgumentNullException(nameof(computePass), "Compute pass cannot be null.");
        }
        SDL_EndGPUComputePass(computePass);
    }

    public static void EndGPUCopyPass(nint copyPass) {
        if (copyPass == nint.Zero) {
            throw new ArgumentNullException(nameof(copyPass), "Copy pass cannot be null.");
        }
        SDL_EndGPUCopyPass(copyPass);
    }

    public static void EndGPURenderPass(nint renderPass) {
        if (renderPass == nint.Zero) {
            throw new ArgumentNullException(nameof(renderPass), "Render pass cannot be null.");
        }
        SDL_EndGPURenderPass(renderPass);
    }

    public static void GenerateMipmapsForGPUTexture(nint commandBuffer, nint texture) {
        if (commandBuffer == nint.Zero) {
            throw new ArgumentNullException(nameof(commandBuffer), "Command buffer cannot be null.");
        }
        if (texture == nint.Zero) {
            throw new ArgumentNullException(nameof(texture), "Texture cannot be null.");
        }
        SDL_GenerateMipmapsForGPUTexture(commandBuffer, texture);
    }

    public static string GetGPUDeviceDriver(nint device) {
        if (device == nint.Zero) {
            throw new ArgumentNullException(nameof(device), "Device cannot be null.");
        }
        return SDL_GetGPUDeviceDriver(device);
    }

    public static string GetGPUDriver(int index) {
        if (index < 0) {
            throw new ArgumentOutOfRangeException(nameof(index), "Index must be non-negative.");
        }
        return SDL_GetGPUDriver(index);
    }

    public static GpuShaderFormat GetGPUShaderFormats(nint device) {
        if (device == nint.Zero) {
            throw new ArgumentNullException(nameof(device), "Device cannot be null.");
        }
        return SDL_GetGPUShaderFormats(device);
    }

    public static GpuTextureFormat GetGPUSwapchainTextureFormat(nint device, nint window) {
        if (device == nint.Zero) {
            throw new ArgumentNullException(nameof(device), "Device cannot be null.");
        }
        if (window == nint.Zero) {
            throw new ArgumentNullException(nameof(window), "Window cannot be null.");
        }
        return SDL_GetGPUSwapchainTextureFormat(device, window);
    }

    public static int GetNumGPUDrivers() {
        return SDL_GetNumGPUDrivers();
    }

    public static bool GPUSupportsProperties(uint props) {
        return SDL_GPUSupportsProperties(props);
    }

    public static bool GPUSupportsShaderFormats(GpuShaderFormat formatFlags, string name) {
        if (!Enum.IsDefined(formatFlags)) {
            throw new ArgumentOutOfRangeException(nameof(formatFlags), "Invalid GpuShaderFormat value.");
        }
        if (string.IsNullOrEmpty(name)) {
            throw new ArgumentException("Name cannot be null or empty.", nameof(name));
        }
        return SDL_GPUSupportsShaderFormats(formatFlags, name);
    }

    public static uint GPUTextureFormatTexelBlockSize(GpuTextureFormat format) {
        if (!Enum.IsDefined(format)) {
            throw new ArgumentOutOfRangeException(nameof(format), "Invalid GpuTextureFormat value.");
        }
        return SDL_GPUTextureFormatTexelBlockSize(format);
    }

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

    public static void InsertGPUDebugLabel(nint commandBuffer, string text) {
        if (commandBuffer == nint.Zero) {
            throw new ArgumentNullException(nameof(commandBuffer), "Command buffer cannot be null.");
        }
        SDL_InsertGPUDebugLabel(commandBuffer, text);
    }

    public static nint MapGPUTransferBuffer(nint device, nint transferBuffer, bool cycle) {
        if (device == nint.Zero) {
            throw new ArgumentNullException(nameof(device), "Device cannot be null.");
        }
        if (transferBuffer == nint.Zero) {
            throw new ArgumentNullException(nameof(transferBuffer), "Transfer buffer cannot be null.");
        }
        return SDL_MapGPUTransferBuffer(device, transferBuffer, cycle);
    }

    public static void PopGPUDebugGroup(nint commandBuffer) {
        if (commandBuffer == nint.Zero) {
            throw new ArgumentNullException(nameof(commandBuffer), "Command buffer cannot be null.");
        }
        SDL_PopGPUDebugGroup(commandBuffer);
    }

    public static void PushGPUComputeUniformData(nint commandBuffer, uint slotIndex, nint data, uint length) {
        if (commandBuffer == nint.Zero) {
            throw new ArgumentNullException(nameof(commandBuffer), "Command buffer cannot be null.");
        }
        SDL_PushGPUComputeUniformData(commandBuffer, slotIndex, data, length);
    }

    public static void PushGPUDebugGroup(nint commandBuffer, string name) {
        if (commandBuffer == nint.Zero) {
            throw new ArgumentNullException(nameof(commandBuffer), "Command buffer cannot be null.");
        }
        SDL_PushGPUDebugGroup(commandBuffer, name);
    }

    public static void PushGPUFragmentUniformData(nint commandBuffer, uint slotIndex, nint data, uint length) {
        if (commandBuffer == nint.Zero) {
            throw new ArgumentNullException(nameof(commandBuffer), "Command buffer cannot be null.");
        }
        SDL_PushGPUFragmentUniformData(commandBuffer, slotIndex, data, length);
    }

    public static void PushGPUVertexUniformData(nint commandBuffer, uint slotIndex, nint data, uint length) {
        if (commandBuffer == nint.Zero) {
            throw new ArgumentNullException(nameof(commandBuffer), "Command buffer cannot be null.");
        }
        SDL_PushGPUVertexUniformData(commandBuffer, slotIndex, data, length);
    }

    public static bool QueryGPUFence(nint device, nint fence) {
        if (device == nint.Zero) {
            throw new ArgumentNullException(nameof(device), "Device cannot be null.");
        }
        if (fence == nint.Zero) {
            throw new ArgumentNullException(nameof(fence), "Fence cannot be null.");
        }
        return SDL_QueryGPUFence(device, fence);
    }

    public static void ReleaseGPUBuffer(nint device, nint buffer) {
        if (device == nint.Zero) {
            throw new ArgumentNullException(nameof(device), "Device cannot be null.");
        }
        if (buffer == nint.Zero) {
            throw new ArgumentNullException(nameof(buffer), "Buffer cannot be null.");
        }
        SDL_ReleaseGPUBuffer(device, buffer);
    }

    public static void ReleaseGPUComputePipeline(nint device, nint computePipeline) {
        if (device == nint.Zero) {
            throw new ArgumentNullException(nameof(device), "Device cannot be null.");
        }
        if (computePipeline == nint.Zero) {
            throw new ArgumentNullException(nameof(computePipeline), "Compute pipeline cannot be null.");
        }
        SDL_ReleaseGPUComputePipeline(device, computePipeline);
    }

    public static void ReleaseGPUFence(nint device, nint fence) {
        if (device == nint.Zero) {
            throw new ArgumentNullException(nameof(device), "Device cannot be null.");
        }
        if (fence == nint.Zero) {
            throw new ArgumentNullException(nameof(fence), "Fence cannot be null.");
        }
        SDL_ReleaseGPUFence(device, fence);
    }

    public static void ReleaseGPUGraphicsPipeline(nint device, nint graphicsPipeline) {
        if (device == nint.Zero) {
            throw new ArgumentNullException(nameof(device), "Device cannot be null.");
        }
        if (graphicsPipeline == nint.Zero) {
            throw new ArgumentNullException(nameof(graphicsPipeline), "Graphics pipeline cannot be null.");
        }
        SDL_ReleaseGPUGraphicsPipeline(device, graphicsPipeline);
    }

    public static void ReleaseGPUSampler(nint device, nint sampler) {
        if (device == nint.Zero) {
            throw new ArgumentNullException(nameof(device), "Device cannot be null.");
        }
        if (sampler == nint.Zero) {
            throw new ArgumentNullException(nameof(sampler), "Sampler cannot be null.");
        }
        SDL_ReleaseGPUSampler(device, sampler);
    }

    public static void ReleaseGPUShader(nint device, nint shader) {
        if (device == nint.Zero) {
            throw new ArgumentNullException(nameof(device), "Device cannot be null.");
        }
        if (shader == nint.Zero) {
            throw new ArgumentNullException(nameof(shader), "Shader cannot be null.");
        }
        SDL_ReleaseGPUShader(device, shader);
    }

    public static void ReleaseGPUTexture(nint device, nint texture) {
        if (device == nint.Zero) {
            throw new ArgumentNullException(nameof(device), "Device cannot be null.");
        }
        if (texture == nint.Zero) {
            throw new ArgumentNullException(nameof(texture), "Texture cannot be null.");
        }
        SDL_ReleaseGPUTexture(device, texture);
    }

    public static void ReleaseGPUTransferBuffer(nint device, nint transferBuffer) {
        if (device == nint.Zero) {
            throw new ArgumentNullException(nameof(device), "Device cannot be null.");
        }
        if (transferBuffer == nint.Zero) {
            throw new ArgumentNullException(nameof(transferBuffer), "Transfer buffer cannot be null.");
        }
        SDL_ReleaseGPUTransferBuffer(device, transferBuffer);
    }

    public static void ReleaseWindowFromGPUDevice(nint device, nint window) {
        if (device == nint.Zero) {
            throw new ArgumentNullException(nameof(device), "Device cannot be null.");
        }
        if (window == nint.Zero) {
            throw new ArgumentNullException(nameof(window), "Window cannot be null.");
        }
        SDL_ReleaseWindowFromGPUDevice(device, window);
    }

    public static bool SetGPUAllowedFramesInFlight(nint device, uint allowedFramesInFlight) {
        if (device == nint.Zero) {
            throw new ArgumentNullException(nameof(device), "Device cannot be null.");
        }
        return SDL_SetGPUAllowedFramesInFlight(device, allowedFramesInFlight);
    }

    public static void SetGPUBlendConstants(nint renderPass, FColor blendConstants) {
        if (renderPass == nint.Zero) {
            throw new ArgumentNullException(nameof(renderPass), "Render pass cannot be null.");
        }
        SDL_SetGPUBlendConstants(renderPass, blendConstants);
    }

    public static void SetGPUBufferName(nint device, nint buffer, string text) {
        if (device == nint.Zero) {
            throw new ArgumentNullException(nameof(device), "Device cannot be null.");
        }
        if (buffer == nint.Zero) {
            throw new ArgumentNullException(nameof(buffer), "Buffer cannot be null.");
        }
        SDL_SetGPUBufferName(device, buffer, text);
    }

    public static void SetGPUScissor(nint renderPass, in Rect scissor) {
        if (renderPass == nint.Zero) {
            throw new ArgumentNullException(nameof(renderPass), "Render pass cannot be null.");
        }
        SDL_SetGPUScissor(renderPass, scissor);
    }

    public static void SetGPUStencilReference(nint renderPass, byte reference) {
        if (renderPass == nint.Zero) {
            throw new ArgumentNullException(nameof(renderPass), "Render pass cannot be null.");
        }
        SDL_SetGPUStencilReference(renderPass, reference);
    }

    public static bool SetGPUSwapchainParameters(nint device, nint window, GpuSwapchainComposition swapchainComposition, GpuPresentMode presentMode) {
        if (device == nint.Zero) {
            throw new ArgumentNullException(nameof(device), "Device cannot be null.");
        }
        if (window == nint.Zero) {
            throw new ArgumentNullException(nameof(window), "Window cannot be null.");
        }
        return SDL_SetGPUSwapchainParameters(device, window, swapchainComposition, presentMode);
    }

    public static void SetGPUTextureName(nint device, nint texture, string text) {
        if (device == nint.Zero) {
            throw new ArgumentNullException(nameof(device), "Device cannot be null.");
        }
        if (texture == nint.Zero) {
            throw new ArgumentNullException(nameof(texture), "Texture cannot be null.");
        }
        SDL_SetGPUTextureName(device, texture, text);
    }

    public static void SetGPUViewport(nint renderPass, in GpuViewport viewport) {
        if (renderPass == nint.Zero) {
            throw new ArgumentNullException(nameof(renderPass), "Render pass cannot be null.");
        }
        SDL_SetGPUViewport(renderPass, viewport);
    }

    public static bool SubmitGPUCommandBuffer(nint commandBuffer) {
        if (commandBuffer == nint.Zero) {
            throw new ArgumentNullException(nameof(commandBuffer), "Command buffer cannot be null.");
        }
        return SDL_SubmitGPUCommandBuffer(commandBuffer);
    }

    public static nint SubmitGPUCommandBufferAndAcquireFence(nint commandBuffer) {
        if (commandBuffer == nint.Zero) {
            throw new ArgumentNullException(nameof(commandBuffer), "Command buffer cannot be null.");
        }
        return SDL_SubmitGPUCommandBufferAndAcquireFence(commandBuffer);
    }

    public static void UnmapGPUTransferBuffer(nint device, nint transferBuffer) {
        if (device == nint.Zero) {
            throw new ArgumentNullException(nameof(device), "Device cannot be null.");
        }
        if (transferBuffer == nint.Zero) {
            throw new ArgumentNullException(nameof(transferBuffer), "Transfer buffer cannot be null.");
        }
        SDL_UnmapGPUTransferBuffer(device, transferBuffer);
    }

    public static void UploadToGPUBuffer(nint copyPass, in GpuTransferBufferLocation source,
        in GpuBufferRegion destination, bool cycle) {
        if (copyPass == nint.Zero) {
            throw new ArgumentNullException(nameof(copyPass), "Copy pass cannot be null.");
        }
        SDL_UploadToGPUBuffer(copyPass, source, destination, cycle);
    }

    public static void UploadToGPUTexture(nint copyPass, in GpuTextureTransferInfo source,
        in GpuTextureRegion destination, bool cycle) {
        if (copyPass == nint.Zero) {
            throw new ArgumentNullException(nameof(copyPass), "Copy pass cannot be null.");
        }
        SDL_UploadToGPUTexture(copyPass, source, destination, cycle);
    }

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

    public static bool WaitForGPUFences(nint device, bool waitAll, Span<nint> fences, uint numFences) {
        if (device == nint.Zero) {
            throw new ArgumentNullException(nameof(device), "Device cannot be null.");
        }
        return SDL_WaitForGPUFences(device, waitAll, fences, numFences);
    }

    public static bool WaitForGPUIdle(nint device) {
        if (device == nint.Zero) {
            throw new ArgumentNullException(nameof(device), "Device cannot be null.");
        }
        return SDL_WaitForGPUIdle(device);
    }

    public static bool WaitForGPUSwapchain(nint device, nint window) {
        if (device == nint.Zero) {
            throw new ArgumentNullException(nameof(device), "Device cannot be null.");
        }
        if (window == nint.Zero) {
            throw new ArgumentNullException(nameof(window), "Window cannot be null.");
        }
        return SDL_WaitForGPUSwapchain(device, window);
    }

    public static bool WindowSupportsGPUPresentMode(nint device, nint window, GpuPresentMode presentMode) {
        if (device == nint.Zero) {
            throw new ArgumentNullException(nameof(device), "Device cannot be null.");
        }
        if (window == nint.Zero) {
            throw new ArgumentNullException(nameof(window), "Window cannot be null.");
        }
        return SDL_WindowSupportsGPUPresentMode(device, window, presentMode);
    }

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