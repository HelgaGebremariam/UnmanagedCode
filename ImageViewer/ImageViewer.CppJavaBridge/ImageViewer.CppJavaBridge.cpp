// ImageViewer.CppJavaBridge.cpp : Defines the exported functions for the DLL application.
//

#include "stdafx.h"
#include <jni.h>


JNIEnv* GetJniEnvironment(JavaVM* jvm)
{
	JNIEnv *env;

	JavaVMInitArgs vm_args;
	JavaVMOption* options = new JavaVMOption[1];

	options[0].optionString = "-Djava.class.path=C:\\Users\\Volha\\Documents\\NetBeansProjects\\ImageProcessor\\dist\\ImageProcessor.jar";
	//options[1].optionString = (char *) "-Djava.compiler=NONE";
	vm_args.version = JNI_VERSION_1_6;
	vm_args.nOptions = 1;
	vm_args.options = options;
	vm_args.ignoreUnrecognized = false;

	jint rc = JNI_CreateJavaVM(&jvm, (void**)&env, &vm_args);
	delete options;
	return env;
}

int* CallIntArrayFunction(JNIEnv *env, char* functionName, int* sourceArray, int height, int width)
{
	jclass cls2 = env->FindClass("ImageEffectsApplier");
	jobject obj1 = env->NewObject(cls2, env->GetMethodID(cls2, "<init>", "()V"));
	if (cls2 == nullptr) {
		return 0;
	}
	else {
		jmethodID mid = env->GetMethodID(cls2, functionName, "([I)[I");
		if (mid == nullptr)
			return 0;
		else {
			jintArray jSourceArray = env->NewIntArray(height * width);
			env->SetIntArrayRegion(jSourceArray, 0, height*width, (jint*)sourceArray);
			jintArray arr = (jintArray)(env->CallObjectMethod(obj1, mid, jSourceArray));
			int length = env->GetArrayLength(arr);
			jboolean isCopy = true;
			int* darr = (int*)env->GetIntArrayElements(arr, &isCopy);
			return darr;
		}
	}
}

__declspec(dllexport) int* MakeImageBlackAndWhite(int* sourceArray, int height, int width)
{
	
	JavaVM *jvm = 0;
	JNIEnv* env = GetJniEnvironment(jvm);
	int* result = CallIntArrayFunction(env, "MakeImageBlackAndWhite", sourceArray, height, width);
	return result;
}