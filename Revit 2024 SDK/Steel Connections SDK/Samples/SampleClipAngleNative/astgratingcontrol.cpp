// Machine generated IDispatch wrapper class(es) created by Microsoft Visual C++

// NOTE: Do not modify the contents of this file.  If this class is regenerated by
//  Microsoft Visual C++, your modifications will be overwritten.


#include "pch.h"
#include "astgratingcontrol.h"

/////////////////////////////////////////////////////////////////////////////
// CAstGratingControl

IMPLEMENT_DYNCREATE(CAstGratingControl, CWnd)

/////////////////////////////////////////////////////////////////////////////
// CAstGratingControl properties

CString CAstGratingControl::GetCurrentSize()
{
	CString result;
	GetProperty(0x6, VT_BSTR, (void*)&result);
	return result;
}

void CAstGratingControl::SetCurrentSize(LPCTSTR propVal)
{
	SetProperty(0x6, VT_BSTR, propVal);
}

CString CAstGratingControl::GetCurrentClass()
{
	CString result;
	GetProperty(0x7, VT_BSTR, (void*)&result);
	return result;
}

void CAstGratingControl::SetCurrentClass(LPCTSTR propVal)
{
	SetProperty(0x7, VT_BSTR, propVal);
}

long CAstGratingControl::GetGratingType()
{
	long result;
	GetProperty(0x8, VT_I4, (void*)&result);
	return result;
}

void CAstGratingControl::SetGratingType(long propVal)
{
	SetProperty(0x8, VT_I4, propVal);
}

long CAstGratingControl::GetShowGratingTypeCombo()
{
	long result;
	GetProperty(0x9, VT_I4, (void*)&result);
	return result;
}

void CAstGratingControl::SetShowGratingTypeCombo(long propVal)
{
	SetProperty(0x9, VT_I4, propVal);
}

long CAstGratingControl::GetLabelLengthAll()
{
	long result;
	GetProperty(0xa, VT_I4, (void*)&result);
	return result;
}

void CAstGratingControl::SetLabelLengthAll(long propVal)
{
	SetProperty(0xa, VT_I4, propVal);
}

long CAstGratingControl::GetSummaryRepresentation()
{
	long result;
	GetProperty(0xb, VT_I4, (void*)&result);
	return result;
}

void CAstGratingControl::SetSummaryRepresentation(long propVal)
{
	SetProperty(0xb, VT_I4, propVal);
}




/////////////////////////////////////////////////////////////////////////////
// CAstGratingControl operations

long CAstGratingControl::GetLabelDBKey(long ctrl)
{
	long result;
	static BYTE parms[] =
		VTS_I4;
	InvokeHelper(0x2, DISPATCH_METHOD, VT_I4, (void*)&result, parms,
		ctrl);
	return result;
}

void CAstGratingControl::SetLabelDBKey(long ctrl, long nNewValue)
{
	static BYTE parms[] =
		VTS_I4 VTS_I4;
	InvokeHelper(0x3, DISPATCH_METHOD, VT_EMPTY, NULL, parms,
		 ctrl, nNewValue);
}

long CAstGratingControl::GetLabelLength(long ctrl)
{
	long result;
	static BYTE parms[] =
		VTS_I4;
	InvokeHelper(0x4, DISPATCH_METHOD, VT_I4, (void*)&result, parms,
		ctrl);
	return result;
}

void CAstGratingControl::SetLabelLength(long ctrl, long nNewValue)
{
	static BYTE parms[] =
		VTS_I4 VTS_I4;
	InvokeHelper(0x5, DISPATCH_METHOD, VT_EMPTY, NULL, parms,
		 ctrl, nNewValue);
}

BOOL CAstGratingControl::GetEnabled()
{
	BOOL result;
	GetProperty(DISPID_ENABLED, VT_BOOL, (void*)&result);
	return result;
}

void CAstGratingControl::SetEnabled(BOOL propVal)
{
	SetProperty(DISPID_ENABLED, VT_BOOL, propVal);
}


BOOL CAstGratingControl::GetAppearance()
{
	BOOL result;
	GetProperty(0xc, VT_BOOL, (void*)&result);
	return result;
}

void CAstGratingControl::SetAppearance(BOOL propVal)
{
	SetProperty(0xc, VT_BOOL, propVal);
}

long CAstGratingControl::GetDropHeight()
{
	long result;
	GetProperty(0xd, VT_I4, (void*)&result);
	return result;
}

void CAstGratingControl::SetDropHeight(long propVal)
{
	SetProperty(0xd, VT_I4, propVal);
}

long CAstGratingControl::GetDropWidth()
{
	long result;
	GetProperty(0xe, VT_I4, (void*)&result);
	return result;
}

void CAstGratingControl::SetDropWidth(long propVal)
{
	SetProperty(0xe, VT_I4, propVal);
}

BOOL CAstGratingControl::GetSummaryDroppedDown()
{
	BOOL result;
	GetProperty(0xf, VT_BOOL, (void*)&result);
	return result;
}

void CAstGratingControl::SetSummaryDroppedDown(BOOL propVal)
{
	SetProperty(0xf, VT_BOOL, propVal);
}

CString CAstGratingControl::GetGridCellDisplayName()
{
	CString result;
	GetProperty(0x10, VT_BSTR, (void*)&result);
	return result;
}

void CAstGratingControl::SetGridCellDisplayName(LPCTSTR propVal)
{
	SetProperty(0x10, VT_BSTR, propVal);
}