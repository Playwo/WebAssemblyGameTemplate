var assemblyName = ""; //IMPORTANT!!! Add that

async function WebAssembly_CreatePlayerAsync() {
    await DotNet.invokeMethodAsync(assemblyName, 'CreatePlayerAsync');
}

async function WebAssembly_UpdatePlayerAsync(playerInfo) {
    await DotNet.invokeMethodAsync(assemblyName, 'UpdatePlayerAsync', playerInfo);
}

async function WebAssembly_LoginStateAsync(loginCode) {
    await DotNet.invokeMethodAsync(assemblyName, 'LoginStateAsync', loginCode);
}

async function WebAssembly_UpdateStateAsync(saveState) {
    await DotNet.invokeMethodAsync(assemblyName, 'UpdateStateAsync', saveState);
}

async function WebAssembly_ActivateStateAsync() {
    await DotNet.invokeMethodAsync(assemblyName, 'ActivateTabAsync');
}