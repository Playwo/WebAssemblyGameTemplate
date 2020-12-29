var assemblyName = ""; //IMPORTANT!!! Add that

async function WebAssembly_CreatePlayerAsync() {
    await DotNet.invokeMethodAsync(assemblyName, 'CreatePlayerAsync');
}

async function WebAssembly_CreatePlayerAsync(playerInfo) {
    await DotNet.invokeMethodAsync(assemblyName, 'UpdatePlayerAsync', playerInfo);
}

async function WebAssembly_CreatePlayerAsync(loginCode) {
    await DotNet.invokeMethodAsync(assemblyName, 'LoginStateAsync', loginCode);
}

async function WebAssembly_CreatePlayerAsync(saveState) {
    await DotNet.invokeMethodAsync(assemblyName, 'UpdateStateAsync', saveState);
}

async function WebAssembly_CreatePlayerAsync() {
    await DotNet.invokeMethodAsync(assemblyName, 'ActivateTabAsync');
}