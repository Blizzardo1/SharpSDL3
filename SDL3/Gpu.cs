using SDL3.Enums;
using SDL3.Structs;
using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

using static SDL3.Sdl;

namespace SDL3; 
public static partial class Gpu {

    [LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_GPUSupportsShaderFormats(GpuShaderFormat formatFlags, string name);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_GPUSupportsProperties(uint props);

    [LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_CreateGPUDevice(GpuShaderFormat formatFlags, SdlBool debugMode, string name);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_CreateGPUDeviceWithProperties(uint props);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_DestroyGPUDevice(nint device);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_GetNumGPUDrivers();

    [LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(OwnedStringMarshaller))]
    private static partial string SDL_GetGPUDriver(int index);

    [LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(OwnedStringMarshaller))]
    private static partial string SDL_GetGPUDeviceDriver(nint device);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial GpuShaderFormat SDL_GetGPUShaderFormats(nint device);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_CreateGPUComputePipeline(nint device,
        in GpuComputePipelineCreateInfo createinfo);

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
    private static partial nint SDL_CreateGPUBuffer(nint device, in GpuBufferCreateInfo createinfo);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_CreateGPUTransferBuffer(nint device,
        in GpuTransferBufferCreateInfo createinfo);

    [LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_SetGPUBufferName(nint device, nint buffer, string text);

    [LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_SetGPUTextureName(nint device, nint texture, string text);

    [LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_InsertGPUDebugLabel(nint commandBuffer, string text);

    [LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_PushGPUDebugGroup(nint commandBuffer, string name);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_PopGPUDebugGroup(nint commandBuffer);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_ReleaseGPUTexture(nint device, nint texture);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_ReleaseGPUSampler(nint device, nint sampler);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_ReleaseGPUBuffer(nint device, nint buffer);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_ReleaseGPUTransferBuffer(nint device, nint transferBuffer);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_ReleaseGPUComputePipeline(nint device, nint computePipeline);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_ReleaseGPUShader(nint device, nint shader);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_ReleaseGPUGraphicsPipeline(nint device, nint graphicsPipeline);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_AcquireGPUCommandBuffer(nint device);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_PushGPUVertexUniformData(nint commandBuffer, uint slotIndex, nint data,
        uint length);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_PushGPUFragmentUniformData(nint commandBuffer, uint slotIndex, nint data,
        uint length);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_PushGPUComputeUniformData(nint commandBuffer, uint slotIndex, nint data,
        uint length);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_BeginGPURenderPass(nint commandBuffer,
        Span<GpuColorTargetInfo> colorTargetInfos, uint numColorTargets,
        in GpuDepthStencilTargetInfo depthStencilTargetInfo);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_BindGPUGraphicsPipeline(nint renderPass, nint graphicsPipeline);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_SetGPUViewport(nint renderPass, in GpuViewport viewport);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_SetGPUScissor(nint renderPass, in Rect scissor);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_SetGPUBlendConstants(nint renderPass, FColor blendConstants);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_SetGPUStencilReference(nint renderPass, byte reference);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_BindGPUVertexBuffers(nint renderPass, uint firstSlot,
        Span<GpuBufferBinding> bindings, uint numBindings);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_BindGPUIndexBuffer(nint renderPass, in GpuBufferBinding binding,
        GpuIndexElementSize indexElementSize);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_BindGPUVertexSamplers(nint renderPass, uint firstSlot,
        Span<GpuTextureSamplerBinding> textureSamplerBindings, uint numBindings);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_BindGPUVertexStorageTextures(nint renderPass, uint firstSlot,
        Span<nint> storageTextures, uint numBindings);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_BindGPUVertexStorageBuffers(nint renderPass, uint firstSlot,
        Span<nint> storageBuffers, uint numBindings);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_BindGPUFragmentSamplers(nint renderPass, uint firstSlot,
        Span<GpuTextureSamplerBinding> textureSamplerBindings, uint numBindings);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_BindGPUFragmentStorageTextures(nint renderPass, uint firstSlot,
        Span<nint> storageTextures, uint numBindings);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_BindGPUFragmentStorageBuffers(nint renderPass, uint firstSlot,
        Span<nint> storageBuffers, uint numBindings);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_DrawGPUIndexedPrimitives(nint renderPass, uint numIndices, uint numInstances,
        uint firstIndex, int vertexOffset, uint firstInstance);

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
    private static partial void SDL_DrawGPUIndexedPrimitivesIndirect(nint renderPass, nint buffer, uint offset,
        uint drawCount);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_EndGPURenderPass(nint renderPass);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_BeginGPUComputePass(nint commandBuffer,
        Span<GpuStorageTextureReadWriteBinding> storageTextureBindings, uint numStorageTextureBindings,
        Span<GpuStorageBufferReadWriteBinding> storageBufferBindings, uint numStorageBufferBindings);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_BindGPUComputePipeline(nint computePass, nint computePipeline);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_BindGPUComputeSamplers(nint computePass, uint firstSlot,
        Span<GpuTextureSamplerBinding> textureSamplerBindings, uint numBindings);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_BindGPUComputeStorageTextures(nint computePass, uint firstSlot,
        Span<nint> storageTextures, uint numBindings);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_BindGPUComputeStorageBuffers(nint computePass, uint firstSlot,
        Span<nint> storageBuffers, uint numBindings);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_DispatchGPUCompute(nint computePass, uint groupcountX, uint groupcountY,
        uint groupcountZ);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_DispatchGPUComputeIndirect(nint computePass, nint buffer, uint offset);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_EndGPUComputePass(nint computePass);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_MapGPUTransferBuffer(nint device, nint transferBuffer, SdlBool cycle);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_UnmapGPUTransferBuffer(nint device, nint transferBuffer);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_BeginGPUCopyPass(nint commandBuffer);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_UploadToGPUTexture(nint copyPass, in GpuTextureTransferInfo source,
        in GpuTextureRegion destination, SdlBool cycle);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_UploadToGPUBuffer(nint copyPass, in GpuTransferBufferLocation source,
        in GpuBufferRegion destination, SdlBool cycle);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_CopyGPUTextureToTexture(nint copyPass, in GpuTextureLocation source,
        in GpuTextureLocation destination, uint w, uint h, uint d, SdlBool cycle);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_CopyGPUBufferToBuffer(nint copyPass, in GpuBufferLocation source,
        in GpuBufferLocation destination, uint size, SdlBool cycle);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_DownloadFromGPUTexture(nint copyPass, in GpuTextureRegion source,
        in GpuTextureTransferInfo destination);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_DownloadFromGPUBuffer(nint copyPass, in GpuBufferRegion source,
        in GpuTransferBufferLocation destination);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_EndGPUCopyPass(nint copyPass);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_GenerateMipmapsForGPUTexture(nint commandBuffer, nint texture);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_BlitGPUTexture(nint commandBuffer, in GpuBlitInfo info);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_WindowSupportsGPUSwapchainComposition(nint device, nint window,
        GpuSwapchainComposition swapchainComposition);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_WindowSupportsGPUPresentMode(nint device, nint window,
        GpuPresentMode presentMode);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_ClaimWindowForGPUDevice(nint device, nint window);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_ReleaseWindowFromGPUDevice(nint device, nint window);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_SetGPUSwapchainParameters(nint device, nint window,
        GpuSwapchainComposition swapchainComposition, GpuPresentMode presentMode);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_SetGPUAllowedFramesInFlight(nint device, uint allowedFramesInFlight);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial GpuTextureFormat SDL_GetGPUSwapchainTextureFormat(nint device, nint window);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_AcquireGPUSwapchainTexture(nint commandBuffer, nint window,
        out nint swapchainTexture, out uint swapchainTextureWidth, out uint swapchainTextureHeight);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_WaitForGPUSwapchain(nint device, nint window);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_WaitAndAcquireGPUSwapchainTexture(nint commandBuffer, nint window,
        out nint swapchainTexture, out uint swapchainTextureWidth, out uint swapchainTextureHeight);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_SubmitGPUCommandBuffer(nint commandBuffer);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_SubmitGPUCommandBufferAndAcquireFence(nint commandBuffer);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_CancelGPUCommandBuffer(nint commandBuffer);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_WaitForGPUIdle(nint device);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_WaitForGPUFences(nint device, SdlBool waitAll, Span<nint> fences,
        uint numFences);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_QueryGPUFence(nint device, nint fence);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_ReleaseGPUFence(nint device, nint fence);

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

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial uint SDL_CalculateGPUTextureFormatSize(GpuTextureFormat format, uint width, uint height,
        uint depthOrLayerCount);
}
